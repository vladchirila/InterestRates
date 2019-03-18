The project is built using Visual Studio 2017. As far as I'm aware, there are no special requirements needed to build and run the aplication.

I am assuming interests will be calculated often, therefore I have added layer of caching on top of the reading of the bands.
I am also assuming the information regarding interest rates is stored in a text file on the disk. In a real-world application though, a more likely place to store that could be the database. Therefore, the text file implementation is somewhat segregated.
