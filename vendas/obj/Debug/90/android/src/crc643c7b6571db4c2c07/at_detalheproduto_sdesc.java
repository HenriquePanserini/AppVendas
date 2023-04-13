package crc643c7b6571db4c2c07;


public class at_detalheproduto_sdesc
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("vendas.at_detalheproduto_sdesc, vendas", at_detalheproduto_sdesc.class, __md_methods);
	}


	public at_detalheproduto_sdesc ()
	{
		super ();
		if (getClass () == at_detalheproduto_sdesc.class) {
			mono.android.TypeManager.Activate ("vendas.at_detalheproduto_sdesc, vendas", "", this, new java.lang.Object[] {  });
		}
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
