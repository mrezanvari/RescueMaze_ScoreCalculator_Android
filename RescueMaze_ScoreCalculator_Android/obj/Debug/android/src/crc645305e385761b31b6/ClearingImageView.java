package crc645305e385761b31b6;


public class ClearingImageView
	extends android.widget.ImageView
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_setImageBitmap:(Landroid/graphics/Bitmap;)V:GetSetImageBitmap_Landroid_graphics_Bitmap_Handler\n" +
			"";
		mono.android.Runtime.register ("SignaturePad.ClearingImageView, SignaturePad", ClearingImageView.class, __md_methods);
	}


	public ClearingImageView (android.content.Context p0)
	{
		super (p0);
		if (getClass () == ClearingImageView.class)
			mono.android.TypeManager.Activate ("SignaturePad.ClearingImageView, SignaturePad", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public ClearingImageView (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == ClearingImageView.class)
			mono.android.TypeManager.Activate ("SignaturePad.ClearingImageView, SignaturePad", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public ClearingImageView (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == ClearingImageView.class)
			mono.android.TypeManager.Activate ("SignaturePad.ClearingImageView, SignaturePad", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public ClearingImageView (android.content.Context p0, android.util.AttributeSet p1, int p2, int p3)
	{
		super (p0, p1, p2, p3);
		if (getClass () == ClearingImageView.class)
			mono.android.TypeManager.Activate ("SignaturePad.ClearingImageView, SignaturePad", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2, p3 });
	}


	public void setImageBitmap (android.graphics.Bitmap p0)
	{
		n_setImageBitmap (p0);
	}

	private native void n_setImageBitmap (android.graphics.Bitmap p0);

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
