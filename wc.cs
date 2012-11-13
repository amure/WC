// It is the project entry. form CPAPA p.63 CN.ver
//[:syn on] [:set nu] [:set cindent]
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
		
		Console.WriteLine("Beginning WordCount program ... ");

		//程序首先添加接受参数的功能，参见12页
		if (args.Length == 0 )
		{
			display_usage();//This function code is in P18.
			return;
		}else{	//There is a code block about foreach in P14
			//bool traceOn = false;
			//bool spyOn = false;
			//写这个区块应该是明显错了，事实上，上面两个布尔量也最好不写在这里
			//string file_name = null; //code from p17

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
					file_name = option;//transmit filename string into file_name
				}
				
			}
		}
		//} this is error if there is a }
		//i want to read traceOn & spyON
		if ( traceOn ) 
			Console.WriteLine( "traceOn = true" );
		else
			Console.WriteLine( "traceOn = false" );
		if ( spyOn ) 
			Console.WriteLine( "spyOn = true" );
		else
			Console.WriteLine( "spyOn = false" );
		//}

		WordCount theObj = new WordCount();
		//transmit filename into processFile
		//theObj.processFile();这里选择传递给这个函数，传递给构造函数的等到了后面在考虑
		//现在这个方式比较简单
		theObj.processFile( file_name );

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
}

public class WordCount
{
	StreamReader freader = null;
	StreamWriter fwriter = null;
	public void processFile(string theFile)//accept a filename from WCE
	{
		openFiles( theFile );
		readFiles();
		countWords();
		getConsoleInput();	//N.010801 P24
		writeWords();
	}

	private void countWords()
	{
		Console.WriteLine( "!!! WordCount.countWords() " );
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
	private void readFiles()
	{
		Console.WriteLine( "!!! WordCount.readFiles() " );
		//read freader that is a StramReader p18
		string text_line;
		int line_cnt = 1; 	//N.01061901
		string [] text_words;	//N.01072101
		char [] separators = {
			' ', '\n', '\t',	//white space
			'.', '\"', ';', ',', '?', '!', ')', '(', '<', '>', '[', ']'
		};			//N.01072101
		ArrayList text = new ArrayList();	//N.011301
		string [][] sentences;	//N.011501
		Hashtable words = new Hashtable();	//N.011601
		Hashtable common_words = new Hashtable();	//N.011602
		common_words.Add( "the", 0 );
		common_words.Add( "but", 0 );
		//...这样子加的方法只能作为一个示例，实际操作需要把这些单词放在一个文件里，然后读取出来
		common_words.Add( "and", 0 );
		while (( text_line = freader.ReadLine() ) != null )
		{
			//dont format empty lines
			if ( text_line.Length == 0 )
			{
				Console.WriteLine();
				continue;
			}//N.01061901
			//write to output file
			//fwriter.WriteLine ( text_line );//N.011302
			//format output to console;
			Console.WriteLine ( "{0} ({2}): {1}", line_cnt++, text_line, text_line.Length );
			//N.01061901
			//hjkl HML Y pP
			//N.01072101
			text_words = text_line.Split( separators );//it was not a good method that use null
			/*foreach ( string option in text_words)
			{
				Console.WriteLine( option );
			}*/
			//insert the line at the back of the container P33
			text.Add( text_line );	//N.011301
			fwriter.WriteLine( text.Capacity );	//N.011302
		}
		//N.011302 trimtosize for arraylist
		text.TrimToSize();
		fwriter.WriteLine( text.Capacity );	//N.011302
		//N.011501
		sentences = new string [ text.Count ][];
		int ix = 0;
		foreach ( string str in text )
		{
			sentences[ ix ] = str.Split( separators );
			++ix;
		}
		//returns length of first dimension ...
		int dim1_length = sentences.GetLength( 0 );
		Console.WriteLine( "There are {0} arrays stored in sentences", dim1_length );
		for ( ix = 0; ix < dim1_length; ++ix )
		{
			Console.WriteLine( "There are {0} words in array {1}", sentences[ ix ].Length, ix+1 );
			foreach ( string s in sentences[ ix ] )
			{
				Console.Write( "{0}", s );

				//N.011601
				// normalize each word to lowercase
				string key = s.ToLower();
				//N.011602 common_words
				if ( common_words.Contains( key ))
					continue;
				// is the word currentli in Hashtable?
				// if not, then we add it ...
				if ( ! words.Contains( key ))
					words.Add( key, 1 );
				// otherwise, we increment the count
				else
					words[ key ] = (int) words[ key ] + 1;
			}
			Console.WriteLine();
		}
		//N.011603
		foreach ( DictionaryEntry de in words )
			fwriter.WriteLine( "{0} : {1}", de.Key, de.Value );
		//must explicitly close the readers
		freader.Close();
		fwriter.Close();
		//end in p18
		//let's see how many we actually assed ... P33
		Console.WriteLine( "We inserted {0} lines", text.Count );//N.011301
	}
	
	private void openFiles(string theFile)//accept a filename from pF
	{
		Console.WriteLine( "!!! WordCount.openFiles() " );

		//copy code from p17
		//StreamReader freader = File.OpenText ( file_name );
		freader = File.OpenText ( theFile ); 
		//StreamWriter fwriter = File.CreateText ( "wc.diag" );
		fwriter = File.CreateText ( "wc.diag" );
		
		//这两个数据成员最简单的就是传递给readFiles，在rF里先实现p18，19里最简单的基础操作
		//先不知用后面教授的方法，就在当前结构下，最简单的解决方法，就是提升freader&fw的访问级别
	}
	
	private void writeWords()
	{
		Console.WriteLine( "!!! WordCount.writeWords() " );
	}

	//N.010801 P24
	//get string from Console INput
	private int getConsoleInput()
	{
		string 	user_name;
		int 	num_tries = 0;
		const int max_tries = 4;

		do
		{
			//generate user message ...
			++num_tries;
			user_name = Console.ReadLine();
			//test whether entry is valid
		}while ( num_tries < max_tries );
		Console.WriteLine ( "hello, {0}", user_name );//print the fouth line input
		return 0;
	}
}
