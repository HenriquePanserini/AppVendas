package crc643c7b6571db4c2c07;


public class at_pesquisacliente1
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
		mono.android.Runtime.register ("vendas.at_pesquisacliente1, vendas", at_pesquisacliente1.class, __md_methods);
	}


	public at_pesquisacliente1 ()
	{
		super ();
		if (getClass () == at_pesquisacliente1.class) {
			mono.android.TypeManager.Activate ("vendas.at_pesquisacliente1, vendas", "", this, new java.lang.Object[] {  });
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
