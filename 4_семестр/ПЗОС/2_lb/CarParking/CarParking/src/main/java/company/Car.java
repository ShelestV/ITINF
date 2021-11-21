package company;

import java.util.Random;

public class Car implements Runnable {
    private final String name;
    private CarStatus status = CarStatus.WAIT;
    private final Parking parking;
    private final int waitingTime = 500;
    private int sumWaitingTime = 0;

    public Car(String name, Parking parking) {
        this.name = name;
        this.parking = parking;
    }

    public String getName() {
        return name;
    }

    public CarStatus getStatusAboutStayingAtParking() {
        return getRandomDecision();
    }

    private CarStatus getRandomDecision() {
        Random randDecision = new Random();
        switch (randDecision.nextInt(2)) {
            case 0 -> status = CarStatus.STAYED;
            case 1 -> status = CarStatus.ARRIVED;
        }
        return status;
    }

    public int getWaitingTime() {
        return waitingTime;
    }

    public void waitTime() {
        this.sumWaitingTime += waitingTime;
    }

    public boolean stillCanNotWait() {
        int waitingLine = 2000;
        return sumWaitingTime > waitingLine;
    }

    public int getStayingAtParkingTime() {
        int stayingAtParkingTime = 1000;
        return stayingAtParkingTime;
    }

    @Override
    public void run() {
        System.out.println(this.getName() + " start to process.");
        try {
            parking.process(this);
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }
}
