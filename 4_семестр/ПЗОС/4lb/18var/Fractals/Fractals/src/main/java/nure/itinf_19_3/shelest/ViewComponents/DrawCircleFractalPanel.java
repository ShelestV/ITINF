package nure.itinf_19_3.shelest.ViewComponents;

import nure.itinf_19_3.shelest.Models.Circle;
import nure.itinf_19_3.shelest.Models.CircleFractal;
import nure.itinf_19_3.shelest.Models.IFractal;

import javax.swing.JPanel;
import java.awt.Graphics;

public class DrawCircleFractalPanel
        extends JPanel {

    private IFractal<Circle> circleFractal;

    public DrawCircleFractalPanel()  {
        circleFractal = null;
    }

    public void setFractal(CircleFractal fractal) {
        circleFractal = fractal;
    }

    @Override
    public void paintComponent(Graphics g) {
        if (circleFractal != null) {
            for (Circle circle : circleFractal.getElements()) {
                g.setColor(circle.getColor());
                g.drawOval(Math.round((float) (circle.getCenter().getX() - circle.getRadius())), // x
                        Math.round((float) (circle.getCenter().getY() - circle.getRadius())),    // y
                        Math.round((float) (circle.getRadius() * 2.0)),                          // width
                        Math.round((float) (circle.getRadius() * 2.0)));                         // height
            }
        }
    }
}
