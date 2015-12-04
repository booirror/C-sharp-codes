
class CT {
	public CT() {
		this.Value = 3;
	}
	public int Value {get;set;}

	static void Main()
	{
		CT ct = new CT();
		System.Console.WriteLine(ct.Value);
	}
}