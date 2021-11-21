package company;

/*Доступно несколько машиномест.
На одном месте может находиться только один автомобиль.
Если все места заняты, то автомобиль не станет ждать больше определенного времени и уедет на другую стоянку.
 */

import java.util.ArrayList;
import java.util.List;

public class Main {

    public static void main(String[] args) {
        List<ParkingPlace> parkingPlaces = new ArrayList<>();
        for (int i = 0; i < 3; ++i) {
            parkingPlaces.add(new ParkingPlace("Place" + i));
        }
        Parking parking = new Parking(parkingPlaces);

        List<Car> cars = new ArrayList<>();
        for (int i = 0; i < 10; ++i) {
            Car car = new Car("Car" + i, parking);
            cars.add(car);
            new Thread(car).start();
        }
    }
}
