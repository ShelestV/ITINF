package nure.itinf_19_3.shelest.Models;

import nure.itinf_19_3.shelest.Resources.Point;
import org.json.simple.JSONObject;

import java.awt.Color;
import java.io.FileWriter;
import java.io.IOException;
import java.io.Serializable;

public class TriangleFractalElement
        implements Serializable {

    private final Triangle interior;
    private final Triangle external;

    public TriangleFractalElement(Point A, Point B, Point C) {
        interior = new Triangle(A, B, C, Color.BLACK);
        external = new Triangle(
                new Point(                                                      // A
                        B.getX() + (C.getX() - B.getX()) / 2.0,
                        C.getY() + (B.getY() - C.getY()) / 2.0),
                new Point(A.getX() + (C.getX() - A.getX()) / 2.0, A.getY()), // B
                new Point(                                                      // C
                        A.getX() + (B.getX() - A.getX()) / 2.0,
                        A.getY() + (B.getY() - A.getY()) / 2.0),
                Color.RED
        );
    }

    private TriangleFractalElement(Triangle external, Triangle interior) {
        this.external = external;
        this.interior = interior;
    }

    public Triangle getExternal() {
        return external;
    }

    public Triangle getInterior() {
        return interior;
    }

    public static TriangleFractalElement getDefault(int width, int height) {
        Point A = new Point(0.0, height);
        Point C = new Point(width, height);

        double h = ((double)width / 2.0) * Math.tan(Math.toRadians(60));
        Point B = new Point((double)width / 2.0, height - h);
        return new TriangleFractalElement(A, B, C);
    }

    public void toJSON(FileWriter writer) throws IOException {
        writer.write("\n{\n \"External\" : \n{");
        external.toJSON(writer);
        writer.write(",\n \"Interior\" : \n{");
        interior.toJSON(writer);
        writer.write("\n}");
    }

    public static TriangleFractalElement getFromJSON (JSONObject json) {
        var external = Triangle.getFromJSON((JSONObject) json.get("External"));
        var interior = Triangle.getFromJSON((JSONObject) json.get("Interior"));
        return new TriangleFractalElement(external, interior);
    }
}
