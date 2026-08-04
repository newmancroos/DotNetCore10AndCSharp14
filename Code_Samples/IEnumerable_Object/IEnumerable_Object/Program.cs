using System.Collections;


var state1 = new State("CA", "California");
var state2 = new State("NY", "New York");

var country = new Country();
country[0] = state1;
country[1] = state2;

foreach (State state in country)
{
    Console.WriteLine($"{state.StateCode} - {state.StateName}");
}

class Country : IEnumerable
{ 
    List<State> states = new List<State>();

    //We don;t need to have Indexor to enumerate a class, here just for indexer sample purpose
    //public Country()
    //{
    //    State state1 = new State("CA", "California");
    //    State state2 = new State("NY", "New York");

    //    states.Add(state1);
    //    states.Add (state2);

    //}

    public State this[int index]
    {
        get { return states[index]; }
        set { states.Insert(index, value); }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return states.GetEnumerator();
    }

}

record State(string StateCode, string StateName);