package company;

/*Доступно несколько машиномест.
На одном месте может находиться только один автомобиль.
Если все места заняты, то автомобиль не станет ждать больше определенного времени и уедет на другую стоянку.
 */

public class ParkingPlace {
    private final String name;
    private boolean isOccupied;
    private PlaceStatus status = PlaceStatus.FREE;

    public ParkingPlace(String name) {
        this.name = name;
        isOccupied = false;
    }

    public void Occupied(Car car) {
        isOccupied = true;
        System.out.println(car.getName() + " is occupying the " + this.name + ".");

        var takingStatus = car.getStatusAboutStayingAtParking();
        switch (takingStatus) {
            case ARRIVED -> {
                System.out.println(car.getName() + " is taking " + this.name + " for some time.");
                this.status = PlaceStatus.TEMPORARY;
            }
            case STAYED -> {
                System.out.println(car.getName() + " is taking " + this.name + " for all day.");
                this.status = PlaceStatus.TAKEN;
            }
            default -> throw new IllegalArgumentException();
        }
    }

    public PlaceStatus getStatus() {
        return status;
    }

    public void free(Car car) {
        isOccupied = false;
        System.out.println(car.getName() + " has arrived from " + this.name + ".");
        status = PlaceStatus.FREE;
    }

    public boolean isFree() {
        return !isOccupied;
    }
}
