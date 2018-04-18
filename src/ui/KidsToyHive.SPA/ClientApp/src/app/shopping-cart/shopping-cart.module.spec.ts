import { ShoppingCartModule } from './shopping-cart.module';

describe('ShoppingCartModule', () => {
  let shoppingCartModule: ShoppingCartModule;

  beforeEach(() => {
    shoppingCartModule = new ShoppingCartModule();
  });

  it('should create an instance', () => {
    expect(shoppingCartModule).toBeTruthy();
  });
});
