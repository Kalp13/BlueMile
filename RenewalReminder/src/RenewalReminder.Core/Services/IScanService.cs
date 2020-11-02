using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RenewalReminder.Core.Services
{
    public interface IScanService
    {
        Task<string> ScanBarcodeAsync(string barcodeName);
    }
}
