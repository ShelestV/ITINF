package nure.itinf_19_3.shelest.Models;

import org.json.simple.JSONArray;
import org.json.simple.JSONObject;

import java.awt.Color;
import java.io.FileWriter;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

public class TriangleFractal
        extends Fractal<TriangleFractalElement> {

    public TriangleFractal(TriangleFractalElement startElement) {
        super(startElement);
    }

    private TriangleFractal(List<TriangleFractalElement> elements) {
        super(elements);
    }

    @Override
    public void transform() {
        List<TriangleFractalElement> newIteration = new ArrayList<>();
        for (TriangleFractalElement element : lastIteration) {
            element.getExternal().setColor(Color.BLACK);
            element.getInterior().setColor(Color.BLACK);

            newIteration.add( // UP
                    new TriangleFractalElement(element.getExternal().getC(),    // A
                                               element.getInterior().getB(),    // B
                                               element.getExternal().getA()));  // C
            newIteration.add( // LEFT
                    new TriangleFractalElement(element.getInterior().getA(),    // A
                                               element.getExternal().getC(),    // B
                                               element.getExternal().getB()));  // C
            newIteration.add( // RIGHT
                    new TriangleFractalElement(element.getExternal().getB(),    // A
                                               element.getExternal().getA(),    // B
                                               element.getInterior().getC()));  // C
        }

        lastIteration = newIteration;
        elements.addAll(newIteration);
    }

    public void toJSON(FileWriter writer) throws IOException {
        writer.write(" {\n \"Elements\" : \n[");
        for (int i = 0; i < elements.size(); ++i) {
            elements.get(i).toJSON(writer);
            writer.write(i != elements.size() - 1 ? ",\n" : " \n] \n} ");
        }
    }

    public static TriangleFractal getFromJSON(JSONObject json) {

        var jsonElements = (JSONArray) json.get("Elements");
        var elementsIterator = jsonElements.iterator();
        var elements = new ArrayList<TriangleFractalElement>();
        while(elementsIterator.hasNext()) {
            elements.add(TriangleFractalElement.getFromJSON((JSONObject) elementsIterator.next()));
        }
        return new TriangleFractal(elements);
    }
}