package crc64e0052b341385bb98;


public class WebAuthenticationCallbackActivity
	extends crc64a0e0a82d0db9a07d.WebAuthenticatorCallbackActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("BlueMile.Certification.Mobile.Droid.WebAuthenticationCallbackActivity, BlueMile.Certification.Mobile.Android", WebAuthenticationCallbackActivity.class, __md_methods);
	}


	public WebAuthenticationCallbackActivity ()
	{
		super ();
		if (getClass () == WebAuthenticationCallbackActivity.class)
			mono.android.TypeManager.Activate ("BlueMile.Certification.Mobile.Droid.WebAuthenticationCallbackActivity, BlueMile.Certification.Mobile.Android", "", this, new java.lang.Object[] {  });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
