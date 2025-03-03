using System.Numbers;

public class PrimeServiceTests
{
  private readonly PrimeService _primeService;

  public PrimeServiceTests()
  {
    _primeService = new PrimeService();
  }

  [Theory]
  [InlineData(-1)]
  [InlineData(0)]
  [InlineData(1)]
  public void IsPrime_ValuesLessThan2_ReturnFalse(int value)
  {
    var result = _primeService.IsPrime(value);
    Assert.False(result);
  }

  [Theory]
  [InlineData(2)]
  [InlineData(3)]
  [InlineData(5)]
  [InlineData(7)]
  public void IsPrime_PrimeNumbers_ReturnTrue(int value)
  {
    var result = _primeService.IsPrime(value);
    Assert.True(result);
  }

  [Theory]
  [InlineData(4)]
  [InlineData(6)]
  [InlineData(8)]
  [InlineData(9)]
  public void IsPrime_NonPrimeNumbers_ReturnFalse(int value)
  {
    var result = _primeService.IsPrime(value);
    Assert.False(result);
  }
}