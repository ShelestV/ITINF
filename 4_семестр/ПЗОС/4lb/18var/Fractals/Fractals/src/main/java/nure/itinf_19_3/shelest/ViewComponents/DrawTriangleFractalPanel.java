package nure.itinf_19_3.shelest.ViewComponents;

import nure.itinf_19_3.shelest.Models.IFractal;
import nure.itinf_19_3.shelest.Models.TriangleFractal;
import nure.itinf_19_3.shelest.Models.TriangleFractalElement;

import javax.swing.JPanel;
import java.awt.Graphics;

public class DrawTriangleFractalPanel
        extends JPanel {

    private IFractal<TriangleFractalElement> triangleFractal;

    public DrawTriangleFractalPanel() {
        triangleFractal = null;
    }

    public void setFractal(TriangleFractal fractal) {
        triangleFractal = fractal;
    }

    @Override
    public void paintComponent(Graphics g) {
        if (triangleFractal != null) {
            for (TriangleFractalElement element : triangleFractal.getElements()) {
                g.setColor(element.getInterior().getColor());
                g.drawPolygon(
                        new int[]{
                                (int) element.getInterior().getA().getX(),
                                (int) element.getInterior().getB().getX(),
                                (int) element.getInterior().getC().getX()
                        },
                        new int[]{
                                (int) element.getInterior().getA().getY(),
                                (int) element.getInterior().getB().getY(),
                                (int) element.getInterior().getC().getY()
                        },
                        3
                );

                g.setColor(element.getExternal().getColor());
                g.drawPolygon(
                        new int[]{
                                (int) element.getExternal().getA().getX(),
                                (int) element.getExternal().getB().getX(),
                                (int) element.getExternal().getC().getX()
                        },
                        new int[]{
                                (int) element.getExternal().getA().getY(),
                                (int) element.getExternal().getB().getY(),
                                (int) element.getExternal().getC().getY()
                        },
                        3
                );
            }
        }
    }
}
