package nure.itinf_19_3.shelest.Server;

import nure.itinf_19_3.shelest.Models.*;

import java.io.*;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.concurrent.Executors;

public class Server {

    private int iteration;

    private IFractal<TriangleFractalElement> triangleFractal;
    private IFractal<Circle> circleFractal;

    public static final int PORT = 8071;

    private Server() throws IOException {
        iteration = 0;
        ServerSocket server = new ServerSocket(PORT);
        while (true) {
            var clientSocket = server.accept();
            processRequest(clientSocket);
            clientSocket.close();
        }
    }

    public void processRequest(Socket socket) throws IOException {
        ++iteration;
        try {
            Thread.sleep(1000);
        } catch (InterruptedException e) {
            e.printStackTrace();
        }

        if (triangleFractal == null || circleFractal == null) {
            var in = new DataInputStream(new BufferedInputStream(socket.getInputStream()));
            int width = in.readInt();
            int height = in.readInt();

            triangleFractal = new TriangleFractal(TriangleFractalElement.getDefault(width, height));
            circleFractal = new CircleFractal(Circle.getDefault(width, height));
        }
        else {
            var executorService = Executors.newFixedThreadPool(2);
            executorService.execute(() -> triangleFractal.transform());
            executorService.execute(() -> circleFractal.transform());
        }

        var writer = new FileWriter("A:\\University\\Fractals\\src\\main\\java\\nure\\itinf_19_3\\shelest\\Fractals.txt", false);
        writer.write("{\n \"Fractals\" : \n[\n ");
        triangleFractal.toJSON(writer);
        writer.write(", \n");
        circleFractal.toJSON(writer);
        writer.write("\n] \n}");
        writer.close();

        var out = new DataOutputStream(new BufferedOutputStream(socket.getOutputStream()));
        out.writeInt(iteration);
        out.flush();
    }

    public static void main(String[] args) {
        try {
            new Server();
        } catch (IOException e) {
            System.err.println("It has been called exception on server");
        }
    }
}
