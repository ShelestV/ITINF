package nure.itinf_19_3.shelest.Models;

import nure.itinf_19_3.shelest.Resources.Point;
import org.json.simple.JSONArray;
import org.json.simple.JSONObject;

import java.awt.Color;
import java.io.FileWriter;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

public class CircleFractal
        extends Fractal<Circle> {

    public CircleFractal(Circle startCircle) {
        super(startCircle);
    }

    private CircleFractal(List<Circle> elements) {
        super(elements);
    }

    @Override
    public void transform() {
        List<Circle> newIteration = new ArrayList<>();
        for (var circle : lastIteration) {
            circle.setColor(Color.BLACK);

            double newRadius = circle.getRadius() / 3.0;
            double distanceFromBigToSmall = circle.getRadius() * (2.0/3.0);
            double xDifference = distanceFromBigToSmall * Math.sin(Math.toRadians(60));
            double yDifference = distanceFromBigToSmall * Math.cos(Math.toRadians(60));

            // Center circle
            newIteration.add(
                    new Circle(circle.getCenter(),
                            newRadius));

            // Other circles are made clockwise
            newIteration.add(
                    new Circle(
                            new nure.itinf_19_3.shelest.Resources.Point(circle.getCenter().getX(),
                                    circle.getCenter().getY() - distanceFromBigToSmall),
                            newRadius));
            newIteration.add(
                    new Circle(
                            new nure.itinf_19_3.shelest.Resources.Point(circle.getCenter().getX() + xDifference,
                                    circle.getCenter().getY() + yDifference),
                            newRadius));
            newIteration.add(
                    new Circle(
                            new Point(circle.getCenter().getX() - xDifference,
                                    circle.getCenter().getY() + yDifference),
                            newRadius));
        }

        elements.addAll(newIteration);
        lastIteration = newIteration;
    }

    public void toJSON(FileWriter writer) throws IOException {
        writer.write(" {\n \"Elements\" : \n[");
        for (int i = 0; i < elements.size(); ++i) {
            elements.get(i).toJSON(writer);
            writer.write(i != elements.size() - 1 ? ", \n" : " \n] \n} ");
        }
    }

    public static CircleFractal getFromJSON(JSONObject json) {

        var jsonElements = (JSONArray) json.get("Elements");
        var elementsIterator = jsonElements.iterator();
        var elements = new ArrayList<Circle>();
        while (elementsIterator.hasNext()) {
            elements.add(Circle.getFromJSON((JSONObject) elementsIterator.next()));
        }
        return new CircleFractal(elements);
    }
}
