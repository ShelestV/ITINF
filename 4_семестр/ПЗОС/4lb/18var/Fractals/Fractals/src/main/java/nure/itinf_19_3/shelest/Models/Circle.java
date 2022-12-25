package nure.itinf_19_3.shelest.Models;

import nure.itinf_19_3.shelest.Resources.Point;
import org.json.simple.JSONObject;

import java.awt.Color;
import java.io.FileWriter;
import java.io.IOException;
import java.io.Serializable;

public class Circle
        implements Serializable {

    private final Point center;
    private final double radius;
    private Color color;

    public Circle(Point center, double radius) {
        this.center = center;
        this.radius = radius;
        color = Color.RED;
    }

    private Circle(Point center, double radius, Color color) {
        this.center = center;
        this.radius = radius;
        this.color = color;
    }

    public Point getCenter() {
        return center;
    }

    public double getRadius() {
        return radius;
    }

    public Color getColor() {
        return color;
    }

    public void setColor(Color color) {
        this.color = color;
    }

    public static Circle getDefault(int width, int height) {
        double radius = Math.min(width, height) / 2.0;
        return new Circle(new Point(radius, radius), radius);
    }

    public void toJSON(FileWriter writer) throws IOException {
        writer.write("{\n \"Center\" : ");
        center.toJSON(writer);
        writer.write(",\n \"Radius\" : " + radius +
                ",\n \"Color\" : { \"Red\" : " + color.getRed() +
                ", \"Green\" : " + color.getGreen() +
                ", \"Blue\" : " + color.getBlue() + " } \n}");
    }

    public static Circle getFromJSON(JSONObject json) {
        var center = Point.getFromJSON((JSONObject) json.get("Center"));
        var radius = (double) json.get("Radius");
        var jsonColor = (JSONObject) json.get("Color");
        long red = (long) jsonColor.get("Red");
        long green = (long) jsonColor.get("Green");
        long blue = (long) jsonColor.get("Blue");
        Color color = new Color((int)red, (int)green, (int)blue);
        return new Circle(center, radius, color);
    }
}
