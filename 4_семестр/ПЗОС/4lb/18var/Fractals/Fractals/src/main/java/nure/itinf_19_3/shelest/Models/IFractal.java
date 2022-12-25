package nure.itinf_19_3.shelest.Models;

import java.io.FileWriter;
import java.io.IOException;
import java.util.List;

public interface IFractal<T> {
    void transform();
    List<T> getElements();
    void toJSON(FileWriter writer) throws IOException;
}
