/*WC, C#编程练习代码，源代码来自于C#Primer中文版的一二章片段
  1、1.17开始框架更加清晰，需要一次更大的改动，改变写有函数功能区块，譬如openFile();
  2、现有代码注释过于很乱，已经有碍于阅读代码；
  3、现在开始的版本姑且叫做练习二版；
  end*/
/*Ver.2
  1、清理注释，参数说明用单行，函数体外用说明用双引注释
  2、实现openFile函数的异常处理功能,N.0117
  3、修改readfile函数架构，多余模块移入countWord，p46
  4、补充WC class数据成员，N.0203 p67
  5、使用property设置m_file_output,p68~69，N.0204
  end for Ver.2*/
/*Ver 2.1
1、使用Indexers访问wordcount里的Hashtable m_words N.20501 p70
2、更改N.010801代码，使其访问wordcount的indexers
*/
using System;
using System.IO;// it is need to read & write a file p17
using System.Collections;//N.011301
public class WordCountEntry
{
	static public void Main( string [] args)
	{
		bool traceOn = false; //There is a code block about foreach in P14
		bool spyOn = false;
		string file_name = null; //code from p17 
		string defaultfile = @"wordCount.txt";//N.0204
		
		Console.WriteLine("Beginning WordCount program ... ");

		//程序参数判断块
		if (args.Length == 0 )
		{
			display_usage();
			return;
		}else{	
			foreach ( string option in args )
			{
				if ( option.Equals ( "-t" ))
					traceOn = true;
				else if (option.Equals ( "-s" ))
					spyOn = true;
				else if (option.Equals ( "-h" ))
				{display_usage(); return;}
				else
				{
					Console.WriteLine( @"check_valid_file_type( option );" );
					file_name = option;
				}
				
			}
		}
		/*这个功能模块判断输入序列中的-t，-s开关，调试用
		if ( traceOn ) 
			Console.WriteLine( "traceOn = true" );
		else
			Console.WriteLine( "traceOn = false" );
		if ( spyOn ) 
			Console.WriteLine( "spyOn = true" );
		else
			Console.WriteLine( "spyOn = false" );
		*/	

		//transmit filename into processFile
		//theObj.processFile();这里选择传递给这个函数，传递给构造函数的等到了后面在考虑
		//现在这个方式比较简单，下一个版本中使用构造函数
		/* 	WordCount theObj = new WordCount();
			theObj.processFile( file_name );	*/
		WordCount theObj = new WordCount( file_name, spyOn, traceOn );
		//N.0204
		if ( theObj.OutFile == null )
			theObj.OutFile = defaultfile;
		theObj.processFile( );

		//N.20501 
		//使用getConsoleInput函数实现WC之index的调用
		if ( getConsoleInput() == "q" ) return;
		Console.WriteLine ( " {0} line(s) match: ", theObj[ getConsoleInput() ] );

		Console.WriteLine( "Ending WordCount program ... ");
	}

	//copy from p18
	public static void display_usage()
	{
		string usage =
			@"usage: WordCount [-s] [-t] [-h] textfile.txt
			where [] indicates an optional argument
			-s prints a series of performance measurements
			-t prints a tracce of the program
			-h prints this message";
		Console.WriteLine( usage );
	}

	//N.010801 P24
	//get string from Console INput
	//这段代码本来是实验Console的输入，对于WC没有功能意义
	//今后即使需要这一个功能也应该将其拿出WC的class使用
	//WC class应该与console无关
	/* getConsoleInput() start
	the end of getConsoleInpYut()*/
	private static string getConsoleInput()
	{
		string 	user_name;
		int 	num_tries = 0;
		const int max_tries = -1;

		Console.Write ( @"Please enter a query or 'q' to Quit: " );
		do
		{
			//generate user message ...
			++num_tries;
			user_name = Console.ReadLine();
			//test whether entry is valid
		}while ( num_tries < max_tries );
		Console.WriteLine ( user_name );
		return user_name ;
	}
}

public class WordCount
{
	//p.67
	private bool 		m_spy;
	private bool 		m_trace;
	
	private string		m_file_name;
	private string		m_file_output;
	
	private StreamReader	m_reader;
	private StreamWriter	m_writer;

	private ArrayList	m_text;
	private Hashtable	m_words;
	
	private string[][]	m_sentences;
	static private char [] 	ms_separators = new char []
	{ ' ', '\n', '\t', '.', '\"', ';', ',', '?', '!', 
		')', '(', '<', '>', '[', ']' };			//N.01072101
	//N.0204
	public string OutFile
	{
		get{ return m_file_output; }
		set
		{
			if ( value.Length != 0 )
				m_file_output = value;
		}
	}

	//N.20501
	public int this[ string index ]
	{
		get
		{
			if ( index.Length == 0 )
				throw new ArgumentException (
						"WordCount: Empty string as index" );
			if ( m_words == null )
				throw new Exception(
						"WordCount: No associated file" );
			return (int) m_words[ index ];
		}
	} 
	
	//p.74~75
	//在没有设计trace模块前，traceFlags使用bool型替换
	public WordCount( string file_name )
		: this( file_name, false, false ) {}

	public WordCount( string file_name, bool spy, bool trace )
	{
		m_file_name = file_name;
		m_spy = spy;
		m_trace = trace;

		if ( m_spy )
			Console.WriteLine( @"m_times = new ArrayList()");

		m_text = new ArrayList();
		m_words = new Hashtable();
	}
	
	public void processFile()
	{
		//m_reader = openFiles( m_file_name );
		readFiles();
		countWords();
		writeWords();
	}

	private void countWords()
	{
		Console.WriteLine( "!!! WordCount.countWords() " );
		
		Hashtable common_words = new Hashtable();	//N.011602
		common_words.Add( "the", 0 );
		common_words.Add( "but", 0 );
		//...这样子加的方法只能作为一个示例，实际操作需要把这些单词放在一个文件里，然后读取出来
		common_words.Add( "and", 0 );
		
		//将text拆成单词，放入sentences这个 jagged array
		//N.011501
		m_sentences = new string [ m_text.Count ][];
		int ix = 0;
		foreach ( string str in m_text )
		{
			m_sentences[ ix ] = str.Split( ms_separators );
			++ix;
		}
		//统计单词，使用的是hashtable words
		int dim1_length = m_sentences.GetLength( 0 ); //returns length of first dimension ...
		Console.WriteLine( "There are {0} arrays stored in sentences", dim1_length );
		for ( ix = 0; ix < dim1_length; ++ix )
		{
			Console.WriteLine( "There are {0} words in array {1}", m_sentences[ ix ].Length, ix+1 );
			foreach ( string s in m_sentences[ ix ] )
			{
				Console.Write( "{0}", s );

				//N.011601
				string key = s.ToLower(); // normalize each word to lowercase
				//N.011602 common_words
				if ( common_words.Contains( key ))
					continue;
				// is the word currently in Hashtable?
				// if not, then we add it ...
				// otherwise, we increment the count
				if ( ! m_words.Contains( key ))
					m_words.Add( key, 1 );
				else
					m_words[ key ] = (int) m_words[ key ] + 1;
			}
			Console.WriteLine();
		}
	}
	
	//1.6 Formatting Output 01-p19 N.01061901
	//writing to the console indicate the line number and the length in characters	
	//N.01072101 split()
	//use the split() method of string
	//N.011301 P33
	//use ArrayList Collection Class
	//N.011302 P34
	//use ArrayList.Capacity
	//本来输入多少，输出多少到文件比较无聊，现在记录A.c的增长比较有意思
	//N.011501 p39
	//use jagged array for save the every words of every line
	//N.011601 p41
	//use Hashtable to hold the occurrence count of tht individual words
	//N.011602 p42
	//keeping track of trivial words with " Hashtable common_words "
	//N.011603 p42
	//print out the occurence count of the words to the output file in dictionary order
	//N.011604 p51e
	//place words.key in a sortable container, use Sort()
	private void readFiles()
	{
		Console.WriteLine( "!!! WordCount.readFiles() " );

		try
		{ 
			string text_line; 
			m_reader = openFiles( m_file_name );
		
			//读取文件中的每一行放入text这个ArrayList中
			while (( text_line = m_reader.ReadLine() ) != null )
			{
				//忽略空行 
				if ( text_line.Length == 0 )
					continue;
				m_text.Add( text_line );	//N.011301
			}
			m_text.TrimToSize(); //N.011302 trimtosize for arraylist
			m_reader.Close();
		}
		catch ( IOException ioe )
		{
			Console.WriteLine ( ioe.ToString() );
		}
		catch ( ArgumentNullException ane )
		{
			Console.WriteLine ( ane.ToString() );
		}
		catch ( ArgumentException ae )
		{
			Console.WriteLine ( ae.ToString() );
		}
		catch ( Exception e )
		{
			Console.WriteLine ( e.ToString() );
		}

		finally
		{
			Console.WriteLine ( "Finally!" );
		}
	}
	
	/*N.0117 the Exception Handling of openFile()
	  code in p44~45
	  end of N.0117*/
	private StreamReader openFiles( string file_name )
	{
		Console.WriteLine( "!!! WordCount.openFiles() " );

		//判断文件名字符串是否存在
		if ( file_name == null )
			throw new ArgumentNullException();
		//判断文件是否存在
		if ( !File.Exists( file_name ))
		{
			string msg = "Invalid file name: " + file_name;
			throw new ArgumentException( msg );
		}
		//判断文件名是否以txt作为扩展名
		if ( !file_name.EndsWith( ".txt" ))
		{
			string msg = "Sorry. ";
			string ext = Path.GetExtension( file_name );

			if ( ext != String.Empty )
				msg += "We currently do not support " + 
					ext + "files.";

			msg = "\nCurrently we only support .txt files.";
			throw new Exception( msg );
		}
		return File.OpenText( file_name );
	}
	
	private void writeWords()
	{
		Console.WriteLine( "!!! WordCount.writeWords() " );
		
		m_writer = File.CreateText( m_file_output );
		//对words进行排序 N.011603
		foreach ( DictionaryEntry de in m_words )
			m_writer.WriteLine( "{0} : {1}", de.Key, de.Value );
		//N.011604
		ArrayList aKeys = new ArrayList( m_words.Keys );
		aKeys.Sort();
		foreach( string key in aKeys )
			m_writer.WriteLine( "{0} :: {1}", key, m_words[ key ]);
		m_writer.Close();

		Console.WriteLine( "We inserted {0} lines", m_text.Count );//N.011301
	}

}
