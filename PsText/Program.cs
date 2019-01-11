using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PsText
{
    class Program
    {
        static void WriteFile(string FilePath)
        {
            File.WriteAllText(FilePath, "if (!([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator))\n" +
            "{\n" +
                "Start - Process powershell.exe \"-NoProfile -ExecutionPolicy Bypass -File `\"$PSCommandPath`\"\" - Verb RunAs\n" +
            "}\n" +

"# Remove the contents under _cache, tracelog and vstsrmlog paths that are old\n" +
"$days = 0\n" +
"$pathToClear = \"D:\\DeleteFolder\"\n" +
"Write - Host \"Removing the cache and logs older than $days days from today under these paths: $pathToClear \"\n" +
"$pathToClear | % { if (Test-Path $_) { Get-ChildItem $_ -Recurse | ? { $_.LastWriteTime -lt(Get-Date).AddDays($days) } | Remove-Item -Force -Recurse -ErrorAction Ignore } }\n" +

"# Remove all \"other agent folders\" that doesn't belong to this agent\n" +
           " #if ($env:ComputerName -eq \"Win10-VM\") { $exclude = \"Azure-Agent\" } else { $exclude = \"$env:ComputerName\" }\n" +
            @"#Get - Item c:\azure - agent,c:\uci - win10 * -Exclude $exclude | Remove - Item - Force - Recurse" +"\n"+

"#Write - Host \"Done cleanup - ($env:ComputerName)\"\n");
        }
        static void Main(string[] args)
        {
            string FilePath = @"D:\v-sulank\cleanup.txt";
            WriteFile(FilePath);
        }
    }
}
