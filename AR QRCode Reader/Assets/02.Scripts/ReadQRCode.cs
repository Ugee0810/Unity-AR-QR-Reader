using UnityEngine;
using ZXing;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class ReadQRCode : MonoBehaviour
{
    public ARCameraManager CameraManager;
    public Image[] images;
    public Text txt;
    public Button button;

    private void Awake()
    {
        button.onClick.AddListener(() =>
        {
            for (int i = 0; i < images.Length; i++) images[i].enabled = false;
            txt.text = "QR코드를 인식해주세요.";
            button.GetComponent<Image>().enabled = false;
            button.interactable = false;
        });
    }

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
                    Images(result.Text);
                }
            }
        }
    }

    void Hide()
    {
        txt.text = "";
        button.GetComponent<Image>().enabled = true;
        button.interactable = true;
    }

    void Images(string result)
    {
        // AR1
        if (result == "http://m.site.naver.com/12K6t")
        {
            Hide();
            images[0].enabled = true;
            images[1].enabled = false;
            images[2].enabled = false;
            images[3].enabled = false;
            images[4].enabled = false;
            images[5].enabled = false;
            images[6].enabled = false;
            images[7].enabled = false;
            images[8].enabled = false;
            images[9].enabled = false;
        }
        // AR2
        if (result == "http://m.site.naver.com/12KfS")
        {
            Hide();
            images[0].enabled = false;
            images[1].enabled = true;
            images[2].enabled = false;
            images[3].enabled = false;
            images[4].enabled = false;
            images[5].enabled = false;
            images[6].enabled = false;
            images[7].enabled = false;
            images[8].enabled = false;
            images[9].enabled = false;
        }
        // AR3
        if (result == "http://m.site.naver.com/12Kg5")
        {
            Hide();
            images[0].enabled = false;
            images[1].enabled = false;
            images[2].enabled = true;
            images[3].enabled = false;
            images[4].enabled = false;
            images[5].enabled = false;
            images[6].enabled = false;
            images[7].enabled = false;
            images[8].enabled = false;
            images[9].enabled = false;
        }
        // AR4
        if (result == "http://m.site.naver.com/12Khb")
        {
            Hide();
            images[0].enabled = false;
            images[1].enabled = false;
            images[2].enabled = false;
            images[3].enabled = true;
            images[4].enabled = false;
            images[5].enabled = false;
            images[6].enabled = false;
            images[7].enabled = false;
            images[8].enabled = false;
            images[9].enabled = false;
        }
        // AR5
        if (result == "http://m.site.naver.com/12Khm")
        {
            Hide();
            images[0].enabled = false;
            images[1].enabled = false;
            images[2].enabled = false;
            images[3].enabled = false;
            images[4].enabled = true;
            images[5].enabled = false;
            images[6].enabled = false;
            images[7].enabled = false;
            images[8].enabled = false;
            images[9].enabled = false;
        }
        // AR6
        if (result == "http://m.site.naver.com/12Khu")
        {
            Hide();
            images[0].enabled = false;
            images[1].enabled = false;
            images[2].enabled = false;
            images[3].enabled = false;
            images[4].enabled = false;
            images[5].enabled = true;
            images[6].enabled = false;
            images[7].enabled = false;
            images[8].enabled = false;
            images[9].enabled = false;
        }
        // AR7
        if (result == "http://m.site.naver.com/12Khz")
        {
            Hide();
            images[0].enabled = false;
            images[1].enabled = false;
            images[2].enabled = false;
            images[3].enabled = false;
            images[4].enabled = false;
            images[5].enabled = false;
            images[6].enabled = true;
            images[7].enabled = false;
            images[8].enabled = false;
            images[9].enabled = false;
        }
        // AR8
        if (result == "http://m.site.naver.com/12KhF")
        {
            Hide();
            images[0].enabled = false;
            images[1].enabled = false;
            images[2].enabled = false;
            images[3].enabled = false;
            images[4].enabled = false;
            images[5].enabled = false;
            images[6].enabled = false;
            images[7].enabled = true;
            images[8].enabled = false;
            images[9].enabled = false;
        }
        // AR9
        if (result == "http://m.site.naver.com/12KhL")
        {
            Hide();
            images[0].enabled = false;
            images[1].enabled = false;
            images[2].enabled = false;
            images[3].enabled = false;
            images[4].enabled = false;
            images[5].enabled = false;
            images[6].enabled = false;
            images[7].enabled = false;
            images[8].enabled = true;
            images[9].enabled = false;
        }
        // AR10
        if (result == "http://m.site.naver.com/12KhR")
        {
            Hide();
            images[0].enabled = false;
            images[1].enabled = false;
            images[2].enabled = false;
            images[3].enabled = false;
            images[4].enabled = false;
            images[5].enabled = false;
            images[6].enabled = false;
            images[7].enabled = false;
            images[8].enabled = false;
            images[9].enabled = true;
        }
    }
}