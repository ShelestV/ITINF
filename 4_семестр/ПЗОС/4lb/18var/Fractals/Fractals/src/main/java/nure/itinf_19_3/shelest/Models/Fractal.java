package nure.itinf_19_3.shelest.Models;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

public abstract class Fractal<T>
        implements IFractal<T>, Serializable {

    protected List<T> elements;
    protected List<T> lastIteration;

    protected Fractal(T startElement) {
        elements = new ArrayList<>();
        elements.add(startElement);
        lastIteration = new ArrayList<>();
        lastIteration.add(startElement);
    }

    protected Fractal(List<T> elements) {
        this.elements = elements;
    }

    @Override
    public abstract void transform();

    @Override
    public List<T> getElements() {
        return elements;
    }
}
