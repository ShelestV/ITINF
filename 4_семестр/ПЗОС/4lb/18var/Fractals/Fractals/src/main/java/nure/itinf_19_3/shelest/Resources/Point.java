package nure.itinf_19_3.shelest.Resources;

import org.json.simple.JSONObject;

import java.io.FileWriter;
import java.io.IOException;
import java.io.Serializable;

public record Point(double x, double y)
        implements Serializable {

    public double getX() {
        return x;
    }

    public double getY() {
        return y;
    }

    public void toJSON(FileWriter writer) throws IOException {
        writer.write("{ \"x\" : " + x + ", \"y\" : " + y + " }");
    }

    public static Point getFromJSON(JSONObject json) {
        double x = (double) json.get("x");
        double y = (double) json.get("y");
        return new Point(x, y);
    }
}
