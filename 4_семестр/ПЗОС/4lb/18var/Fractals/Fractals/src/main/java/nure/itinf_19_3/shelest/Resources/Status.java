package nure.itinf_19_3.shelest.Resources;

public class Status {
    private boolean isWork;

    public Status() {
        isWork = false;
    }

    public void work() {
        isWork = true;
    }

    public void doesNotWork() {
        isWork = false;
    }

    public boolean isWork() {
        return isWork;
    }
}
