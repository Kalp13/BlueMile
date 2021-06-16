using BlueMile.Certification.Web.ApiModels;
using BlueMile.Certification.Web.ApiModels.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace BlueMile.Certification.Web.ApiClient
{
    public class CertificationApiClient : ICertificationApiClient
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance of <see cref="CertificationApiClient"/>.
        /// </summary>
        /// <param name="baseAddress"></param>
        /// <param name="token"></param>
        public CertificationApiClient(string baseAddress, string token)
        {
            this.baseAddress = baseAddress ?? throw new ArgumentNullException(nameof(baseAddress));
            this.userToken = token ?? throw new ArgumentNullException(nameof(token));
        }

        #endregion

        #region ICertification Implementation

        #region User Methods

        /// <inheritdoc/>
        public async Task<bool> RegisterUser(UserRegistrationModel userModel)
        {
            try
            {
                if (this.client == null)
                {
                    this.client = this.CreateClient();
                }

                var json = JsonConvert.SerializeObject(userModel, typeof(UserRegistrationModel), Formatting.None, null);
                using (StringContent content = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    HttpResponseMessage response = null;

                    Uri uri = new Uri($@"{this.baseAddress}/Users/register");

                    response = await client.PostAsync(uri, content);

                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<UserToken> LogUserIn(UserLoginModel userModel)
        {
            try
            {
                if (this.client == null)
                {
                    this.client = this.CreateClient();
                }

                var request = new HttpRequestMessage(HttpMethod.Post, $@"{this.baseAddress}/Users/login");
                request.Content = new StringContent(JsonConvert.SerializeObject(userModel), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    throw new ArgumentException(response.ReasonPhrase);
                }

                var content = await response.Content.ReadAsStringAsync();
                var token = JsonConvert.DeserializeObject<UserToken>(content);

                this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.Token);

                return token;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> IsServiceActive()
        {
            try
            {
                if (this.client == null)
                {
                    this.client = CreateClient();
                }
                this.client.Timeout = TimeSpan.FromSeconds(10);

                Uri uri = new Uri($@"{this.baseAddress}/Certification/ping");
                HttpResponseMessage response = await this.client.GetAsync(uri);
                
                return response.IsSuccessStatusCode;
            }
            catch (TaskCanceledException)
            {
                return false;
            }
            catch (HttpRequestException)
            {
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Owner Methods

        /// <inheritdoc/>
        public async Task<PagedResponseModel<OwnerModel>> FindOwners(FindOwnerModel filterModel)
        {
            try
            {
                if (filterModel == null)
                {
                    throw new ArgumentNullException(nameof(filterModel));
                }

                if (this.client == null)
                {
                    this.client = CreateClient();
                }

                var addressBuilder = new UriBuilder($"{this.baseAddress}/Certification/owner");
                var query = HttpUtility.ParseQueryString(addressBuilder.Query);
                query[nameof(filterModel.OwnerId)] = filterModel.OwnerId.ToString();
                query[nameof(filterModel.SearchTerm)] = filterModel.SearchTerm;

                addressBuilder.Query = query.ToString();

                var authHeader = new AuthenticationHeaderValue("bearer", this.userToken);
                this.client.DefaultRequestHeaders.Authorization = authHeader;

                var request = new HttpRequestMessage(HttpMethod.Get, addressBuilder.ToString());
                request.Headers.Authorization = authHeader;

                var response = await this.client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var owner = JsonConvert.DeserializeObject<PagedResponseModel<OwnerModel>>(content);
                    return owner;
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<OwnerModel> GetOwnerBySystemId(Guid ownerId)
        {
            try
            {
                if (ownerId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(ownerId));
                }

                if (this.client == null)
                {
                    this.client = CreateClient();
                }

                var filterModel = new FindOwnerModel()
                {
                    OwnerId = ownerId,
                    SearchTerm = ""
                };
                var addressBuilder = new UriBuilder($"{this.baseAddress}/Certification/owner");
                var query = HttpUtility.ParseQueryString(addressBuilder.Query);
                query[nameof(filterModel.OwnerId)] = filterModel.OwnerId.ToString();
                query[nameof(filterModel.SearchTerm)] = filterModel.SearchTerm;

                addressBuilder.Query = query.ToString();

                var authHeader = new AuthenticationHeaderValue("bearer", this.userToken);
                this.client.DefaultRequestHeaders.Authorization = authHeader;

                var request = new HttpRequestMessage(HttpMethod.Get, addressBuilder.ToString());
                request.Headers.Authorization = authHeader;

                var response = await this.client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var owner = JsonConvert.DeserializeObject<IEnumerable<OwnerModel>>(content);
                    return owner.FirstOrDefault();
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<OwnerModel> GetOwnerByUsername(string username)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(username))
                {
                    throw new ArgumentNullException(nameof(username));
                }

                if (this.client == null)
                {
                    this.client = CreateClient();
                }

                Uri uri = new Uri($@"{this.baseAddress}/Certification/owner/get/{username}");
                HttpResponseMessage response = await this.client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var owner = JsonConvert.DeserializeObject<OwnerModel>(content);
                    return owner;
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<Guid> CreateOwner(OwnerModel owner)
        {
            try
            {
                if (owner == null)
                {
                    throw new ArgumentNullException(nameof(owner));
                }

                if (this.client == null)
                {
                    this.client = CreateClient();
                }
                var idDoc = owner.IdentificationDocument;
                var skippersDoc = owner.SkippersLicenseImage;
                var icasaDoc = owner.IcasaPopPhoto;

                string json = JsonConvert.SerializeObject(OwnerHelper.ToUpdateOwnerModel(owner));
                using MultipartFormDataContent allContent = new MultipartFormDataContent();

                using StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                allContent.Add(content);

                if (idDoc != null && !String.IsNullOrWhiteSpace(idDoc.FilePath))
                {
                    var idDocData = File.ReadAllBytes(idDoc.FilePath);
                    var idDocContent = new StreamContent(new MemoryStream(idDocData));
                    idDocContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = "\"files\"",
                        FileName = "\"" + idDoc.FileName + "\"",
                    }; // the extra quotes are key here
                    idDocContent.Headers.ContentType = new MediaTypeHeaderValue(idDoc.MimeType);
                    idDocContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("Id", idDoc.Id.ToString()));
                    idDocContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("Name", idDoc.FileName.Replace(" ", "_")));
                    idDocContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("Type", idDoc.DocumentTypeId.ToString()));

                    allContent.Add(idDocContent);
                }

                if (skippersDoc != null && !String.IsNullOrWhiteSpace(skippersDoc.FilePath))
                {
                    var skippersDocData = File.ReadAllBytes(skippersDoc.FilePath);
                    var skippersDocContent = new StreamContent(new MemoryStream(skippersDocData));
                    skippersDocContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = "\"files\"",
                        FileName = "\"" + skippersDoc.FileName + "\"",
                    }; // the extra quotes are key here
                    skippersDocContent.Headers.ContentType = new MediaTypeHeaderValue(skippersDoc.MimeType);
                    skippersDocContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("Id", skippersDoc.Id.ToString()));
                    skippersDocContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("Name", skippersDoc.FileName.Replace(" ", "_")));
                    skippersDocContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("Type", skippersDoc.DocumentTypeId.ToString()));

                    allContent.Add(skippersDocContent);
                }

                if (icasaDoc != null && !String.IsNullOrWhiteSpace(icasaDoc.FilePath))
                {
                    var icasaDocData = File.ReadAllBytes(icasaDoc.FilePath);
                    var icasaDocContent = new StreamContent(new MemoryStream(icasaDocData));
                    icasaDocContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = "\"files\"",
                        FileName = "\"" + icasaDoc.FileName + "\"",
                    }; // the extra quotes are key here
                    icasaDocContent.Headers.ContentType = new MediaTypeHeaderValue(icasaDoc.MimeType);
                    icasaDocContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("Id", icasaDoc.Id.ToString()));
                    icasaDocContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("Name", icasaDoc.FileName.Replace(" ", "_")));
                    icasaDocContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("Type", icasaDoc.DocumentTypeId.ToString()));

                    allContent.Add(icasaDocContent);
                }

                HttpResponseMessage response = null;
                Uri uri = new Uri($@"{this.baseAddress}/Certification/owner/create");
                response = await this.client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var ownerId = JsonConvert.DeserializeObject<Guid>(result);
                    return ownerId;
                }
                else
                {
                    throw new ArgumentException(response.ReasonPhrase);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<Guid> UpdateOwner(OwnerModel owner)
        {
            try
            {
                if (owner == null)
                {
                    throw new ArgumentNullException(nameof(owner));
                }

                if (this.client == null)
                {
                    this.client = CreateClient();
                }
                this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", this.userToken);
                var idDoc = owner.IdentificationDocument;
                var skippersDoc = owner.SkippersLicenseImage;
                var icasaDoc = owner.IcasaPopPhoto;

                string json = JsonConvert.SerializeObject(OwnerHelper.ToUpdateOwnerModel(owner));
                using MultipartFormDataContent allContent = new MultipartFormDataContent();
                
                using StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                allContent.Add(content);

                if (idDoc != null && !String.IsNullOrWhiteSpace(idDoc.FilePath))
                {
                    var idDocData = File.ReadAllBytes(idDoc.FilePath);
                    var idDocContent = new StreamContent(new MemoryStream(idDocData));
                    idDocContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = "\"files\"",
                        FileName = "\"" + idDoc.FileName + "\"",
                    }; // the extra quotes are key here
                    idDocContent.Headers.ContentType = new MediaTypeHeaderValue(idDoc.MimeType);
                    idDocContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("Id", idDoc.Id.ToString()));
                    idDocContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("Name", idDoc.FileName.Replace(" ", "_")));
                    idDocContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("Type", idDoc.DocumentTypeId.ToString()));

                    allContent.Add(idDocContent);
                }

                if (skippersDoc != null && !String.IsNullOrWhiteSpace(skippersDoc.FilePath))
                {
                    var skippersDocData = File.ReadAllBytes(skippersDoc.FilePath);
                    var skippersDocContent = new StreamContent(new MemoryStream(skippersDocData));
                    skippersDocContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = "\"files\"",
                        FileName = "\"" + skippersDoc.FileName + "\"",
                    }; // the extra quotes are key here
                    skippersDocContent.Headers.ContentType = new MediaTypeHeaderValue(skippersDoc.MimeType);
                    skippersDocContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("Id", skippersDoc.Id.ToString()));
                    skippersDocContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("Name", skippersDoc.FileName.Replace(" ", "_")));
                    skippersDocContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("Type", skippersDoc.DocumentTypeId.ToString()));

                    allContent.Add(skippersDocContent);
                }

                if (icasaDoc != null && !String.IsNullOrWhiteSpace(icasaDoc.FilePath))
                {
                    var icasaDocData = File.ReadAllBytes(icasaDoc.FilePath);
                    var icasaDocContent = new StreamContent(new MemoryStream(icasaDocData));
                    icasaDocContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = "\"files\"",
                        FileName = "\"" + icasaDoc.FileName + "\"",
                    }; // the extra quotes are key here
                    icasaDocContent.Headers.ContentType = new MediaTypeHeaderValue(icasaDoc.MimeType);
                    icasaDocContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("Id", icasaDoc.Id.ToString()));
                    icasaDocContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("Name", icasaDoc.FileName.Replace(" ", "_")));
                    icasaDocContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("Type", icasaDoc.DocumentTypeId.ToString()));

                    allContent.Add(icasaDocContent);
                }

                HttpResponseMessage response = null;
                Uri uri = new Uri($@"{this.baseAddress}/Certification/owner/update/{owner.Id}");
                response = await this.client.PutAsync(uri, allContent);

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
                    return result;
                }
                else
                {
                    throw new ArgumentException(response.ReasonPhrase);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Boat Methods

        /// <inheritdoc/>
        public async Task<List<BoatModel>> GetBoatsByOwnerId(Guid ownerId)
        {
            try
            {
                if (ownerId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(ownerId));
                }

                if (this.client == null)
                {
                    this.client = CreateClient();
                }
                this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", this.userToken);

                Uri uri = new Uri($@"{this.baseAddress}/Certification/boat/{ownerId}");
                HttpResponseMessage response = await this.client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var boats = JsonConvert.DeserializeObject<List<BoatModel>>(content);
                    return boats.ToList();
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<BoatModel> GetBoatById(Guid boatId)
        {
            try
            {
                if (boatId == null || boatId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(boatId));
                }

                if (this.client == null)
                {
                    this.client = CreateClient();
                }
                this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", this.userToken);

                HttpResponseMessage response = null;

                Uri uri = new Uri($@"{this.baseAddress}/Certification/boat/get/{boatId}");
                if (this.client.BaseAddress == null)
                {
                    this.client.BaseAddress = new Uri(this.baseAddress);
                }

                response = await this.client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<BoatModel>(await response.Content.ReadAsStringAsync());
                    return result;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw new ArgumentException(response.ReasonPhrase);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<Guid> CreateBoat(BoatModel boat)
        {
            try
            {
                if (boat == null)
                {
                    throw new ArgumentNullException(nameof(boat));
                }

                if (this.client == null)
                {
                    this.client = CreateClient();
                }
                this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", this.userToken);
                var boyancyDoc = boat.BoyancyCertificateImage;
                var tubbiesDoc = boat.TubbiesCertificateImage;

                string json = JsonConvert.SerializeObject(BoatHelper.ToCreateBoatModel(boat));
                using MultipartFormDataContent allContent = new MultipartFormDataContent();

                using StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                allContent.Add(content);

                if (boyancyDoc != null && !String.IsNullOrWhiteSpace(boyancyDoc.FilePath))
                {
                    var boyancyDocData = File.ReadAllBytes(boyancyDoc.FilePath);
                    var boyancyDocContent = new StreamContent(new MemoryStream(boyancyDocData));
                    boyancyDocContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = "\"files\"",
                        FileName = "\"" + boyancyDoc.FileName + "\"",
                    }; // the extra quotes are key here
                    boyancyDocContent.Headers.ContentType = new MediaTypeHeaderValue(boyancyDoc.MimeType);
                    boyancyDocContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("Id", boyancyDoc.Id.ToString()));
                    boyancyDocContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("Name", boyancyDoc.FileName.Replace(" ", "_")));
                    boyancyDocContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("Type", boyancyDoc.DocumentTypeId.ToString()));

                    allContent.Add(boyancyDocContent);
                }
                if (tubbiesDoc != null && !String.IsNullOrWhiteSpace(tubbiesDoc.FilePath))
                {
                    var tubbiesDocData = File.ReadAllBytes(tubbiesDoc.FilePath);
                    var tubbiesDocContent = new StreamContent(new MemoryStream(tubbiesDocData));
                    tubbiesDocContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = "\"files\"",
                        FileName = "\"" + tubbiesDoc.FileName + "\"",
                    }; // the extra quotes are key here
                    tubbiesDocContent.Headers.ContentType = new MediaTypeHeaderValue(tubbiesDoc.MimeType);
                    tubbiesDocContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("Id", tubbiesDoc.Id.ToString()));
                    tubbiesDocContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("Name", tubbiesDoc.FileName.Replace(" ", "_")));
                    tubbiesDocContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("Type", tubbiesDoc.DocumentTypeId.ToString()));

                    allContent.Add(tubbiesDocContent);
                }

                HttpResponseMessage response = null;
                Uri uri = new Uri($@"{this.baseAddress}/Certification/boat/create");

                response = await this.client.PostAsync(uri, allContent);

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
                    return result;
                }
                else
                {
                    throw new ArgumentException(response.ReasonPhrase);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<Guid> UpdateBoat(BoatModel boat)
        {
            try
            {
                if (boat == null)
                {
                    throw new ArgumentNullException(nameof(boat));
                }

                if (this.client == null)
                {
                    this.client = CreateClient();
                }
                this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", this.userToken);
                var boyancyDoc = boat.BoyancyCertificateImage;
                var tubbiesDoc = boat.TubbiesCertificateImage;

                string json = JsonConvert.SerializeObject(BoatHelper.ToUpdateBoatModel(boat));
                using MultipartFormDataContent allContent = new MultipartFormDataContent();

                using StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                allContent.Add(content);

                if (boyancyDoc != null && !String.IsNullOrWhiteSpace(boyancyDoc.FilePath))
                {
                    var boyancyDocData = File.ReadAllBytes(boyancyDoc.FilePath);
                    var boyancyDocContent = new StreamContent(new MemoryStream(boyancyDocData));
                    boyancyDocContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = "\"files\"",
                        FileName = "\"" + boyancyDoc.FileName + "\"",
                    }; // the extra quotes are key here
                    boyancyDocContent.Headers.ContentType = new MediaTypeHeaderValue(boyancyDoc.MimeType);
                    boyancyDocContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("Id", boyancyDoc.Id.ToString()));
                    boyancyDocContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("Name", boyancyDoc.FileName.Replace(" ", "_")));
                    boyancyDocContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("Type", boyancyDoc.DocumentTypeId.ToString()));

                    allContent.Add(boyancyDocContent);
                }
                if (tubbiesDoc != null && !String.IsNullOrWhiteSpace(tubbiesDoc.FilePath))
                {
                    var tubbiesDocData = File.ReadAllBytes(tubbiesDoc.FilePath);
                    var tubbiesDocContent = new StreamContent(new MemoryStream(tubbiesDocData));
                    tubbiesDocContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = "\"files\"",
                        FileName = "\"" + tubbiesDoc.FileName + "\"",
                    }; // the extra quotes are key here
                    tubbiesDocContent.Headers.ContentType = new MediaTypeHeaderValue(tubbiesDoc.MimeType);
                    tubbiesDocContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("Id", tubbiesDoc.Id.ToString()));
                    tubbiesDocContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("Name", tubbiesDoc.FileName.Replace(" ", "_")));
                    tubbiesDocContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("Type", tubbiesDoc.DocumentTypeId.ToString()));

                    allContent.Add(tubbiesDocContent);
                }

                HttpResponseMessage response = null;
                Uri uri = new Uri($@"{this.baseAddress}/Certification/boat/update/{boat.Id}");

                response = await this.client.PutAsync(uri, allContent);

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
                    return result;
                }
                else
                {
                    throw new ArgumentException(response.ReasonPhrase);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> DoesBoatExist(Guid boatId)
        {
            try
            {
                if (boatId == null || boatId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(boatId));
                }

                if (this.client == null)
                {
                    this.client = CreateClient();
                }
                this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", this.userToken);

                HttpResponseMessage response = null;

                Uri uri = new Uri($@"{this.baseAddress}/Certification/boat/exists/{boatId}");
                if (this.client.BaseAddress == null)
                {
                    this.client.BaseAddress = new Uri(this.baseAddress);
                }

                response = await this.client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
                    return result;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return false;
                }
                else
                {
                    throw new ArgumentException(response.ReasonPhrase);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Item Methods

        /// <inheritdoc/>
        public async Task<List<ItemModel>> GetBoatRequiredItems(Guid boatId)
        {
            try
            {
                if (boatId == null || boatId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(boatId));
                }

                if (this.client == null)
                {
                    this.client = CreateClient();
                }
                this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", this.userToken);

                Uri uri = new Uri($"{this.baseAddress}/Certification/item/{boatId}");
                HttpResponseMessage response = await this.client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var items = JsonConvert.DeserializeObject<List<ItemModel>>(content);
                    return items;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<ItemModel> GetItemById(Guid itemId)
        {
            try
            {
                if (itemId == null || itemId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(itemId));
                }

                if (this.client == null)
                {
                    this.client = CreateClient();
                }
                this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", this.userToken);

                Uri uri = new Uri($"{this.baseAddress}/Certification/item/get/{itemId}");
                HttpResponseMessage response = await this.client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var items = JsonConvert.DeserializeObject<ItemModel>(content);
                    return items;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<Guid> CreateItem(ItemModel item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException(nameof(item));
                }

                if (this.client == null)
                {
                    this.client = CreateClient();
                }
                this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", this.userToken);
                var itemDoc = item.ItemImage;

                string json = JsonConvert.SerializeObject(ItemHelper.ToCreateItemModel(item));
                using MultipartFormDataContent allContent = new MultipartFormDataContent();

                using StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                allContent.Add(content);

                if (itemDoc != null && !String.IsNullOrWhiteSpace(itemDoc.FilePath))
                {
                    var itemDocData = File.ReadAllBytes(itemDoc.FilePath);
                    var itemDocContent = new StreamContent(new MemoryStream(itemDocData));
                    itemDocContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = "\"files\"",
                        FileName = "\"" + itemDoc.FileName + "\"",
                    }; // the extra quotes are key here
                    itemDocContent.Headers.ContentType = new MediaTypeHeaderValue(itemDoc.MimeType);
                    itemDocContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("Id", itemDoc.Id.ToString()));
                    itemDocContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("Name", itemDoc.FileName.Replace(" ", "_")));
                    itemDocContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("Type", itemDoc.DocumentTypeId.ToString()));

                    allContent.Add(itemDocContent);
                }

                HttpResponseMessage response = null;

                Uri uri = new Uri($"{this.baseAddress}/Certification/item/create");
                if (this.client.BaseAddress == null)
                {
                    this.client.BaseAddress = new Uri(this.baseAddress);
                }

                response = await client.PostAsync(uri, allContent);

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
                    return result;
                }
                else
                {
                    throw new ArgumentException(response.ReasonPhrase);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<Guid> UpdateItem(ItemModel item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException(nameof(item));
                }

                if (this.client == null)
                {
                    this.client = CreateClient();
                }
                this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", this.userToken);
                var itemDoc = item.ItemImage;

                string json = JsonConvert.SerializeObject(ItemHelper.ToUpdateItemModel(item));
                using MultipartFormDataContent allContent = new MultipartFormDataContent();

                using StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                allContent.Add(content);

                if (itemDoc != null && !String.IsNullOrWhiteSpace(itemDoc.FilePath))
                {
                    var itemDocData = File.ReadAllBytes(itemDoc.FilePath);
                    var itemDocContent = new StreamContent(new MemoryStream(itemDocData));
                    itemDocContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = "\"files\"",
                        FileName = "\"" + itemDoc.FileName + "\"",
                    }; // the extra quotes are key here
                    itemDocContent.Headers.ContentType = new MediaTypeHeaderValue(itemDoc.MimeType);
                    itemDocContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("Id", itemDoc.Id.ToString()));
                    itemDocContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("Name", itemDoc.FileName.Replace(" ", "_")));
                    itemDocContent.Headers.ContentDisposition.Parameters.Add(new NameValueHeaderValue("Type", itemDoc.DocumentTypeId.ToString()));

                    allContent.Add(itemDocContent);
                }

                HttpResponseMessage response = null;

                Uri uri = new Uri($"{this.baseAddress}/Certification/item/update/{item.Id}");
                if (this.client.BaseAddress == null)
                {
                    this.client.BaseAddress = new Uri(this.baseAddress);
                }

                response = await this.client.PutAsync(uri, allContent);

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<Guid>(await response.Content.ReadAsStringAsync());
                    return result;
                }
                else
                {
                    throw new ArgumentException(response.ReasonPhrase);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<bool> DoesItemExist(Guid itemId)
        {
            try
            {
                if (itemId == null || itemId == Guid.Empty)
                {
                    throw new ArgumentNullException(nameof(itemId));
                }

                if (this.client == null)
                {
                    this.client = CreateClient();
                }
                this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", this.userToken);

                HttpResponseMessage response = null;

                Uri uri = new Uri($@"{this.baseAddress}/Certification/item/exists/{itemId}");
                if (this.client.BaseAddress == null)
                {
                    this.client.BaseAddress = new Uri(this.baseAddress);
                }

                response = await this.client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
                    return result;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return false;
                }
                else
                {
                    throw new ArgumentException(response.ReasonPhrase);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #endregion

        #region Instance Methods

        private HttpClient CreateClient()
        {
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; },
            };
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
            HttpClient client = new HttpClient(httpClientHandler, false);
            client.BaseAddress = new Uri($"{this.baseAddress}");
            client.Timeout = TimeSpan.FromMinutes(5);
            return client;
        }

        #endregion

        private HttpClient client;

        private string baseAddress;

        private string userToken;
    }

    public static class HttpRequestExtensions
    {
        private static string TimeoutPropertyKey = "RequestTimeout";

        public static void SetTimeout(
            this HttpRequestMessage request,
            TimeSpan? timeout)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            request.Properties[TimeoutPropertyKey] = timeout;
        }

        public static TimeSpan? GetTimeout(this HttpRequestMessage request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Properties.TryGetValue(
                    TimeoutPropertyKey,
                    out var value)
                && value is TimeSpan timeout)
                return timeout;
            return null;
        }
    }

    class TimeoutHandler : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
