# Code Convention & Style

# General style

- Cases: [https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/identifier-names](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/identifier-names)

```csharp
// Vi bruger PascalCase til type names, namespace og alle public members
namespace SampleNamespace 
{ // Opening curly brackets on their own line 
	public class SampleClass
	{
		// Fields first
		// public string FieldSample; // FORBIDDEN! Make auto property instead
		private readonly string sampleField; // camelCase for private fields
		private string backingFieldSample;

		// Properties second
		public string AutoPropertySample { get; set; } // Spaces before/after curly braces
		public string PropertySample 
		{
			get => backingFieldSample; // Arrow functions ok
			set
			{
				// Some validation
				backingFieldSample = value;
			}
		}
		
		// Constructor(s) third
		public SampleClass(string sample, string sample, int id = 0) 
		{
			Sample = sample; // Assign to public var to get validation
			AutoPropertySample = sample; // If(readonly property) assignment to private var is ok
		}

		// Methods fourth
		public void MethodName(string paramName1, int paramName2)
		{
			private readstring someString = "Some string";
			// Some code...
		}

		public void ExprMethod(string a) => a;
	}
}

```

- Variabler skal initialiseres explicit. `int i = 0`
- Maksimal længde pr. linje: 100
- Undgå forkortelser medmindre de er alment kendt og accepteret
- Maksimalt have en class pr. dokument.
- Skal vi have en regel om at bruge mellemrum imellem symboler og variabler?
- Primært skriv type explicit ellers brug `var` hvis typenavnet er for langt.

```csharp
// for-løkke format

for (int i = 0; i < 10; i++)
```

- Undgå for så vidt muligt nestede for-loops

# Variables

- Variable navne skal være sigende, ikke noget: temp1, temp2 osv. Hellere lange variable-navne end lange kommentarer.
- Erklær variabler uafhængigt og ikke i samme statement.
- Ingen underscores foran private instance fields.
- Private instance variables skal skrives over property.
- Eksplicit erklær access modifier

```csharp
// Forkert!
void PrintMeddelelse(string message)
{…}
// Rigtig!
private void PrintMeddelelse(string message)
{…}
```

# Documentation

## Comments

- Bruger vi //, /// eller /*  */ til kommentering? Og gør vi noget forskelligt i forhold til kommentarer på en eller flere linjer?
- Vi laver mellemrum imellem kommentarer og //

```csharp
// This is a comment
```

- Som udgangspunkt laver vi kommentater på en separat linje.

```csharp
// This is a comment explaining this variable
public int someNumber 
```

## UML documentation

Bare følg:

[https://docs.microsoft.com/en-us/dotnet/csharp/codedoc](https://docs.microsoft.com/en-us/dotnet/csharp/codedoc)

# Error handling

- Implementeres hvor det findes nødvendigt

# Classes

- Én klasse per fil (genvejstast til dette: Alt+Enter ⇒ ArrowDown ⇒ Enter
