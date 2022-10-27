using UnityEngine;
using ZXing;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class ReadQRCode : MonoBehaviour
{
    public ARCameraManager CameraManager;
    public Text txt;

    void Update()
    {
        if (CameraManager.TryAcquireLatestCpuImage(out XRCpuImage image))
        {
            using (image)
            {
                var conversionParams = new XRCpuImage.ConversionParams(image, TextureFormat.R8, XRCpuImage.Transformation.MirrorY);
                var dataSize = image.GetConvertedDataSize(conversionParams);
                var grayscalePixels = new byte[dataSize];

                unsafe
                {
                    fixed (void* ptr = grayscalePixels)
                    {
                        image.Convert(conversionParams, new System.IntPtr(ptr), dataSize);
                    }
                }

                IBarcodeReader barcodeReader = new BarcodeReader();
                var result = barcodeReader.Decode(grayscalePixels, image.width, image.height, RGBLuminanceSource.BitmapFormat.Gray8);

                if (result != null)
                {
                    txt.text = result.Text;
                    SetObject(); //�Լ� ȣ�� �߰�
                }
            }
        }
    }

    /// <summary>
    /// ������Ʈ ���� �Լ� 
    /// </summary>
    void SetObject()
    {
        GetComponent<ObjectPlacement>().enabled = true;
        ObjectPlacement.Instance.qrcode = "Object";
    }
}