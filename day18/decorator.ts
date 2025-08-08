function LogMethod(
target:Object,  
propertyKey: string,
descriptor: PropertyDescriptor
) {
  const originalMethod = descriptor.value;
  descriptor.value = function (...args: any[]) {
    console.log(`Calling ${propertyKey} with`, args);
    return originalMethod.apply(this, args);
  };
}

class Calculator {
  @LogMethod //  Unable to resolve signature of method decorator when called
  add(a: number, b: number): number {
    return a + b;
  }
}

const calc = new Calculator();
calc.add(2, 3); // Logs: Calling add with [2, 3]
