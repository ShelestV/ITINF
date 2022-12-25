package transformation;

import java.awt.Image;
import java.awt.Graphics;
import java.awt.image.PixelGrabber;
import java.awt.image.BufferedImage;
import java.awt.image.WritableRaster;

import java.io.File;
import java.io.IOException;
import javax.imageio.ImageIO;

import java.util.Random;

public class Transfomation {
    private Image originalImage;

    private Image changedImage;
    private Image gausImage;
    private Image laplasGausImage;

    private int imageWidth;
    private int imageHeight;

    private double[][] gausian = {
        { 0.0029, 0.0131, 0.0215, 0.0131, 0.0029 },
        { 0.0131, 0.0585, 0.0965, 0.0585, 0.0131 },
        { 0.0215, 0.0965, 0.1592, 0.0965, 0.0215 },
        { 0.0131, 0.0585, 0.0965, 0.0585, 0.0131 },
        { 0.0029, 0.0131, 0.0215, 0.0131, 0.0029 }
    };

    public void init(String imagePath) {
        BufferedImage image = null;
        try {
            image = ImageIO.read(new File(imagePath));
        } catch (IOException ex) { 
            System.out.println(ex.getMessage());
        }

        changedImage = change(image);
        gausImage = gaus(image);
    }

    public void paint(Graphics graphics) {
        graphics.drawImage(originalImage, 0, 0, null);
        graphics.drawImage(changedImage, 0, imageHeight + 10, null);
        graphics.drawImage(gausImage, imageWidth + 10, 0, null);
        graphics.drawImage(laplasGausImage, imageWidth + 10, imageHeight + 10, null);

    }

    private Image change(Image image) {
        imageWidth = image.getWidth(null);
        imageHeight = image.getHeight(null);

        var pixels = new int[imageWidth * imageHeight];

        var grabber = new PixelGrabber(image, 0, 0, imageWidth, imageHeight, pixels, 0, imageWidth);

        try {
            grabber.grabPixels();
        } catch (InterruptedException ex) {
            System.out.println(ex.getMessage());
            return null;
        }

        for (var i = 0; i < pixels.length; i++) {
            pixels[i] += Integer.parseInt(randHex(), 16);
        }

        return getImageFromArray(pixels, imageWidth, imageHeight - 4);
    }

    private Image gaus(Image image) {
        imageWidth = image.getWidth(null);
        imageHeight = image.getHeight(null);

        var pixels = new int[imageWidth * imageHeight];
        var pixelsMinus4 = new int[imageWidth * imageHeight - 4];

        var grabber = new PixelGrabber(image, 0, 0, imageWidth, imageHeight, pixels, 0, imageWidth);

        try {
            grabber.grabPixels();
        } catch (InterruptedException ex) {
            System.out.println(ex.getMessage());
            return null;
        }

        for (int pixelsIndex = 0, k = 0; pixelsIndex < pixelsMinus4.length; pixelsIndex++) {
            k = pixelsIndex;
            for (int i = 0; i < gausian.length; i++) {
                for (int j = 0; j < gausian[i].length; j++) {
                    try {
                        pixelsMinus4[pixelsIndex] += pixels[k] * gausian[i][j];
                    } catch (ArrayIndexOutOfBoundsException ex) {
                        System.out.println(ex.getMessage());
                    }
                }
                k++;
            }
        }

        return getImageFromArray(pixelsMinus4, imageWidth, imageHeight - 4);
    }

    private String randHex() {
        var random = new Random();
        String[] hexArray = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };
        var hexNumber = "";

        for (var i = 0; i < 6; i++) {
            hexNumber += hexArray[random.nextInt(16)];
        }

        return hexNumber;
    }

    private static Image getImageFromArray(int[] pixels, int width, int height) {
        var image = new BufferedImage(width, height, BufferedImage.TYPE_INT_ARGB);
        var raster = (WritableRaster)image.getData();
        try {
            raster.setPixels(0, 0, width + 1, height + 1, pixels);
        } catch (ArrayIndexOutOfBoundsException ex) {
            System.out.println(ex.getMessage());
        }
        image.setData(raster);
        return image;
    }
}
