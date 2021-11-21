package company;

/*Доступно несколько машиномест.
На одном месте может находиться только один автомобиль.
Если все места заняты, то автомобиль не станет ждать больше определенного времени и уедет на другую стоянку.
 */

import java.util.List;

public class Parking implements Runnable {
    private final List<ParkingPlace> parkingPlaces;

    Parking(List<ParkingPlace> parkingPlaces) {
        this.parkingPlaces = parkingPlaces;
    }

    private ParkingPlace findFreePlaceOrNull() {
        for (var parkingPlace : parkingPlaces) {
            if (parkingPlace.isFree()) {
                return parkingPlace;
            }
        }
        return null;
    }

    public synchronized void process(Car car) throws InterruptedException {
        ParkingPlace freePlace;

        while ((freePlace = findFreePlaceOrNull()) == null) {
            System.out.println(car.getName() + " is waiting.");
            car.waitTime();
            wait(car.getWaitingTime());

            if (car.stillCanNotWait()) {
                System.out.println(car.getName() + " is going away due to long waiting.");
                return;
            }
        }

        freePlace.Occupied(car);
        if (freePlace.getStatus() == PlaceStatus.TEMPORARY) {
            wait(car.getStayingAtParkingTime());
            freePlace.free(car);
        }
        notify();
    }

    @Override
    public void run() {

    }
}

