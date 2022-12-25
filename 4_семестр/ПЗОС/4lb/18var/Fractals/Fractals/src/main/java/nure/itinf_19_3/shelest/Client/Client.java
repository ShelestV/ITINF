package nure.itinf_19_3.shelest.Client;

import nure.itinf_19_3.shelest.Models.CircleFractal;
import nure.itinf_19_3.shelest.Models.TriangleFractal;
import nure.itinf_19_3.shelest.Resources.Iteration;
import nure.itinf_19_3.shelest.Resources.Status;
import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;

import java.io.*;
import java.net.InetAddress;
import java.net.Socket;
import java.util.Date;
import java.util.concurrent.Executors;

public class Client implements IKeySendListener {

    private final ClientFrame frame;

    private long kickOffTimeMsec;

    private final static int SEND_TIMEOUT_MSEC = 10 * 1000; // 10 seconds
    private final static int SERVER_PORT = 8071;

    private final Status status;
    private final Iteration iteration;

    @Override
    public synchronized void send() {
        notify();
    }

    private boolean timeExpired() {
        return (new Date().getTime() - kickOffTimeMsec) > SEND_TIMEOUT_MSEC;
    }

    private Client() {
        status = new Status();
        iteration = new Iteration();

        kickOffTimeMsec = new Date().getTime();
        frame = new ClientFrame(status, this, iteration);
        frame.setVisible(true);

        while (true) {
            synchronized (this) {
                try {
                    wait();
                } catch (InterruptedException e) {
                    System.out.println("Interrupted main thread");
                }
                // check time
                if (timeExpired()) {

                    if (!status.isWork())
                        continue;

                    try {
                        SendRequestToServer();
                        GetFractalsFromServer();
                    } catch (IOException e) {
                        System.err.println("Failed to send data to server");
                    } catch (ClassNotFoundException e) {
                        System.err.println("Cannot convert data from server");
                    } catch (ParseException e) {
                        System.err.println("Something is wrong with json");
                    }

                    kickOffTimeMsec = new Date().getTime();

                } else {
                    System.out.println("Wait a bit...");
                }
            }
        }
    }

    private void SendRequestToServer() throws IOException {
        Socket socket = new Socket(InetAddress.getLocalHost(), SERVER_PORT);

        DataOutputStream ds = new DataOutputStream(new BufferedOutputStream(socket.getOutputStream()));
        ds.writeInt(250);
        ds.writeInt(250);
        ds.flush();

        socket.close();
    }

    private void GetFractalsFromServer() throws IOException, ClassNotFoundException, ParseException {

        while (status.isWork()) {
            var socket = new Socket(InetAddress.getLocalHost(), SERVER_PORT);
            var in = new DataInputStream(new BufferedInputStream(socket.getInputStream()));
            iteration.setIteration(in.readInt());

            var reader = new FileReader("A:\\University\\Fractals\\src\\main\\java\\nure\\itinf_19_3\\shelest\\Fractals.txt");
            JSONObject json = (JSONObject) new JSONParser().parse(reader);
            JSONArray fractals = (JSONArray) json.get("Fractals");
            reader.close();

            var executorService = Executors.newFixedThreadPool(2);
            executorService.execute(() -> {
                frame.setTriangleFractalPanel(TriangleFractal.getFromJSON((JSONObject) fractals.get(0)));
            });
            executorService.execute(() -> {
                frame.setCircleFractalPanel(CircleFractal.getFromJSON((JSONObject) fractals.get(1)));
            });

            socket.close();
        }
    }

    public static void main(String[] args) {
        new Client();
    }
}
