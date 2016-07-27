/*
 MIT License

Copyright (c) [2016] [Orlin Dimitrov]

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
 */

using System;

using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

// Thi project is without deadline it can take overtime,
// no need extra payman.

namespace StereoScopic
{
    /// <summary>
    /// This class is dedecated to transform images by the anagliph techology,
    /// to cyan red 3D images. 
    /// </summary>
    // 20140918 JI@DevGroup: Добра практика е името на класа да е същото като името на файла
    public class Anaglyph
    {
        #region Private



        #endregion

        #region Public

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetImage"></param>
        /// <param name="shift"></param>
        /// <returns></returns>
        public static Bitmap Make3DPopIn(Image targetImage, int shift)
        {
            return Make3DPopIn((Bitmap)targetImage, shift);
        }

        /// <summary>
        /// Convert normal image to a 3D Cyan Red techology image.
        /// </summary>
        /// <param name="targetImage">Target image.</param>
        /// <param name="shift">Shift - the offset between left and right buffers.</param>
        /// <returns>The 3D image.</returns>
        public static Bitmap Make3DPopIn(Bitmap targetImage, int shift)
        {
            // This would be easier if we had COI support for cv.Set, but it doesn't
            // work that way.
            // OpenCV uses BGR order (even if input image is greyscale):
            // http://www.cs.iit.edu/~agam/cs512/lect-notes/opencv-intro/opencv-intro.html
            // red goes on the left, cyan on the right:
            // # http://en.wikipedia.org/wiki/Anaglyph_image

            // Create base image.
            Image<Bgr, Byte> inputImage = new Image<Bgr, byte>(targetImage);

            // Get the size.
            Size inputImageSize = inputImage.Size;

            // Output image.
            Image<Bgr, Byte> anaglyph;// = new Image<Bgr, byte>(inputImageSize);

            // Devide the image by chanels.
            // Image<Gray, Byte> blueImage = new Image<Gray, byte>(inputImage[0].ToBitmap());
            // Image<Gray, Byte> greenImage = new Image<Gray, byte>(inputImage[1].ToBitmap());
            // Image<Gray, Byte> redImage = new Image<Gray, byte>(inputImage[2].ToBitmap());

            Image<Gray, Byte> blueImage  = inputImage[0];
            Image<Gray, Byte> greenImage = inputImage[1];
            Image<Gray, Byte> redImage   = inputImage[2];

            // Empty image.
            Image<Gray, byte> zeros = new Image<Gray, byte>(inputImageSize);

            // Create the output images.
            Image<Bgr, Byte> leftImage = new Image<Bgr, byte>(new Image<Gray, byte>[] { zeros, zeros, redImage });
            Image<Bgr, Byte> rightImage = new Image<Bgr, byte>(new Image<Gray, byte>[] { blueImage, greenImage, zeros });

            // Set the shift and ROI (view).
            leftImage.ROI  = new Rectangle(new Point(shift, 0), new Size(inputImageSize.Width - shift, inputImageSize.Height));
            rightImage.ROI = new Rectangle(new Point(0,     0), new Size(inputImageSize.Width - shift, inputImageSize.Height));

            // Generate output image.
            anaglyph = leftImage.Add(rightImage);

            return anaglyph.ToBitmap(); //resizeImage(anaglyph.ToBitmap(), outputSize);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetImage"></param>
        /// <param name="shift"></param>
        /// <returns></returns>
        public static Bitmap Make3DPopOut(Image targetImage, int shift)
        {
            return Make3DPopOut((Bitmap)targetImage, shift);
        }

        /// <summary>
        /// Convert normal image to a 3D Cyan Red techology image.
        /// </summary>
        /// <param name="targetImage">Target image.</param>
        /// <param name="shift">Shift - the offset between left and right buffers.</param>
        /// <returns></returns>
        public static Bitmap Make3DPopOut(Bitmap targetImage, int shift)
        {
            // This would be easier if we had COI support for cv.Set, but it doesn't
            // work that way.
            // OpenCV uses BGR order (even if input image is greyscale):
            // http://www.cs.iit.edu/~agam/cs512/lect-notes/opencv-intro/opencv-intro.html
            // red goes on the left, cyan on the right:
            // # http://en.wikipedia.org/wiki/Anaglyph_image

            // Create base image.
            Image<Bgr, Byte> inputImage = new Image<Bgr, byte>(targetImage);

            // Get the size.
            Size inputImageSize = inputImage.Size;

            // Output image.
            Image<Bgr, Byte> anaglyph;// = new Image<Bgr, byte>(inputImageSize);

            // Devide the image by chanels.
            // Image<Gray, Byte> blueImage = new Image<Gray, byte>(inputImage[0].ToBitmap());
            // Image<Gray, Byte> greenImage = new Image<Gray, byte>(inputImage[1].ToBitmap());
            // Image<Gray, Byte> redImage = new Image<Gray, byte>(inputImage[2].ToBitmap());

            Image<Gray, Byte> blueImage = inputImage[0];
            Image<Gray, Byte> greenImage = inputImage[1];
            Image<Gray, Byte> redImage = inputImage[2];

            // Empty image.
            Image<Gray, byte> zeros = new Image<Gray, byte>(inputImageSize);

            // Create the output images.
            Image<Bgr, Byte> leftImage = new Image<Bgr, byte>(new Image<Gray, byte>[] { zeros, zeros, redImage });
            Image<Bgr, Byte> rightImage = new Image<Bgr, byte>(new Image<Gray, byte>[] { blueImage, greenImage, zeros });

            // Set the shift and ROI (view).
            rightImage.ROI = new Rectangle(new Point(shift, 0), new Size(inputImageSize.Width - shift, inputImageSize.Height));
            leftImage.ROI = new Rectangle(new Point(0, 0), new Size(inputImageSize.Width - shift, inputImageSize.Height));

            // Generate output image.
            anaglyph = leftImage.Add(rightImage);

            return anaglyph.ToBitmap(); //resizeImage(anaglyph.ToBitmap(), outputSize);
        }

        /// <summary>
        /// Takes two images from 1 stereo camera and make it in Cyan Red technology in 3D.
        /// </summary>
        /// <param name="targetImageLeft">Left image.</param>
        /// <param name="targetImageRight">Right image.</param>
        /// <param name="outputSize">Output image size.</param>
        /// <returns>Output image.</returns>
        public static Bitmap Make3DFrom2Images(Bitmap targetImageLeft, Bitmap targetImageRight)
        {
            // This would be easier if we had COI support for cv.Set, but it doesn't
            // work that way.
            // OpenCV uses BGR order (even if input image is greyscale):
            // http://www.cs.iit.edu/~agam/cs512/lect-notes/opencv-intro/opencv-intro.html
            // red goes on the left, cyan on the right:
            // # http://en.wikipedia.org/wiki/Anaglyph_image

            if (!targetImageLeft.Size.Equals((Size)targetImageRight.Size))
            {
                throw new Exception("Not equals image sizes.");
            }

            // Create base image.
            Image<Bgr, Byte> inputLeftImage  = new Image<Bgr, byte>(targetImageLeft);
            Image<Bgr, Byte> inputRightImage = new Image<Bgr, byte>(targetImageRight);

            // Create the output images.
            Image<Bgr, Byte> outputImage = new Image<Bgr, byte>(new Image<Gray, byte>[] { inputRightImage[0], inputRightImage[1], inputLeftImage[2] });

            // Give bach the image.
            return outputImage.ToBitmap();
        }

        #endregion
    }
}
