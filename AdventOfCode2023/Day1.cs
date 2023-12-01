namespace AdventOfCode2023;

[TestClass]
public class Day1
{
	[TestMethod]
	public void Part1( )
	{
		IEnumerable< string > contents = ReadFile( "PuzzleInput" );

		int sum = 0;
		foreach ( string content in contents )
		{
			IEnumerable< char > numbers = content.Where( Char.IsNumber );
			string num = $"{numbers.First( )}{numbers.Last( )}";
			sum += int.Parse( num );
		}

		Console.WriteLine( $"The sum is: {sum}" );
	}

	private Dictionary< string, int > ValidDigits = new( )
	                                                {
		                                                {"one", 1 }, 
		                                                {"two",2},
		                                                {"three",3},
		                                                {"four",4},
		                                                {"five",5},
		                                                {"six",6},
		                                                {"seven",7},
		                                                {"eight",8},
		                                                {"nine",9},
		                                                {"1",1},
		                                                {"2",2},
		                                                {"3",3},
		                                                {"4",4},
		                                                {"5",5},
		                                                {"6",6},
		                                                {"7",7},
		                                                {"8",8},
		                                                {"9",9},
	                                                };

	[TestMethod]
	public void Part2( )
	{
		IEnumerable< string > contents = ReadFile( "PuzzleInput" );

		int sum = Part2_Logic( contents );
		Console.WriteLine( $"The sum is: {sum}" );
	}

	[TestMethod]
	public void Part2_Test( )
	{
		List< string > lines = new List< string >( )
		                       {
			                       "one2threee"
		                       };
		int sum = Part2_Logic( lines );

		Assert.AreEqual(
			13,
			sum );
	}

	private int Part2_Logic(
		IEnumerable< string > lines )
	{
		int sum = 0;
		foreach ( string line in lines )
		{
			List< KeyValuePair< string, int > > digits = new List< KeyValuePair< string, int > >( );
			for ( int i = 0;
			      i < line.Length;
			      i++ )
			{
				for ( int j = i + 1;
				      j <= line.Length;
				      j++ )
				{
					KeyValuePair< string, int > number = ValidDigits.FirstOrDefault( vd => vd.Key == line[ i..j ] );
					if ( number.Key == null )
					{
						continue;
					}
					digits.Add( number );
				}
			}

			string num = $"{digits.First( ).Value}{digits.Last( ).Value}";
			sum += int.Parse( num );
		}
		return sum;
	}

	private static IEnumerable< string > ReadFile(
		string name )
	{
		if ( !File.Exists( name ) )
		{
			throw new Exception( "The file is not there!" );
		}

		string[ ] contents = File.ReadAllLines( name );
		return contents;
	}
}