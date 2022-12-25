package nure.itinf_19_3.shelest.Models;

import nure.itinf_19_3.shelest.Resources.Point;

import java.awt.Color;
import java.io.FileWriter;
import java.io.IOException;
import java.io.Serializable;

import org.json.simple.JSONObject;

public class Triangle
        implements Serializable {

    private final Point A;
    private final Point B;
    private final Point C;
    private Color color;

    public Triangle(Point A, Point B, Point C, Color color) {
        this.A = A;
        this.B = B;
        this.C = C;
        this.color = color;
    }

    public Point getA() {
        return A;
    }

    public Point getB() {
        return B;
    }

    public Point getC() {
        return C;
    }

    public Color getColor() {
        return color;
    }

    public void setColor(Color color) {
        this.color = color;
    }

    public void toJSON(FileWriter writer) throws IOException {
        writer.write("\n \"A\" : ");
        A.toJSON(writer);
        writer.write(",\n \"B\" : ");
        B.toJSON(writer);
        writer.write(",\n \"C\" : ");
        C.toJSON(writer);
        writer.write(",\n \"Color\" : { \"Red\" : " + color.getRed() +
                ", \"Green\" : " + color.getGreen() +
                ", \"Blue\" : " + color.getBlue() + " } \n}");

    }

    public static Triangle getFromJSON(JSONObject json) {
        Point A = Point.getFromJSON((JSONObject) json.get("A"));
        Point B = Point.getFromJSON((JSONObject) json.get("B"));
        Point C = Point.getFromJSON((JSONObject) json.get("C"));
        var jsonColor = (JSONObject) json.get("Color");
        long red = (long) jsonColor.get("Red");
        long green = (long) jsonColor.get("Green");
        long blue = (long) jsonColor.get("Blue");
        Color color = new Color((int)red, (int)green, (int)blue);
        return new Triangle(A, B, C, color);
    }
}
