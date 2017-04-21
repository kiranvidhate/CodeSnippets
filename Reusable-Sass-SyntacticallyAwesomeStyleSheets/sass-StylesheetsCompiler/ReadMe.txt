**** Prerequiresites JDK 6 and ABOVE ***

Downloaded from: https://mhs.github.io/scout-app/
Getting started Video: https://www.youtube.com/watch?v=Fju3aXW6zLM&feature=youtu.be

To Fix following error
scout ArgumentError: Error #3214

find the file process_interaction.js and edit its javaDir() function as follows:

function javaDir()
{
path = air.File.applicationDirectory.resolvePath("C:\ProgramData\Oracle\Java\javapath\java.exe");
return path;
}

set java exe path appropriately 


SASS Documentation: http://sass-lang.com/documentation/file.SASS_REFERENCE.html
