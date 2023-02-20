namespace PaymentSystem.Calculators

module HourlyPaymentCalculator =
    let calculate (hourlyPayment: float, hours: int) =
        hourlyPayment * float hours