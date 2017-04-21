using System;
using System.Reflection;

// Encrypt resources (and compress any *.txt resource)
[assembly: Obfuscation(Feature = "encrypt resources", Exclude = false)]
[assembly: Obfuscation(Feature = "encrypt resources [compress] *.txt", Exclude = false)]

// Encrypt symbol names with a password
[assembly: Obfuscation(Feature = "encrypt symbol names with password", Exclude = false)]

// Embed ITCLog.dll into the assembly
[assembly: Obfuscation(Feature = "embed ITCLog.dll", Exclude = false)]

// Embed Utilities.dll into the assembly
[assembly: Obfuscation(Feature = "embed Utilities.dll", Exclude = false)]

// Embed AES256Ext.dll into the assembly
[assembly: Obfuscation(Feature = "embed AES256Ext.dll", Exclude = false)]

// Embed NetlinkPacket.dll into the assembly
[assembly: Obfuscation(Feature = "embed NetlinkPacket.dll", Exclude = false)]

// Merge Infralution.Licensing.Forms.dll into the assembly
[assembly: Obfuscation(Feature = "merge with Infralution.Licensing.Forms.dll", Exclude = false)]

// Embed Globals.dll into the assembly
[assembly: Obfuscation(Feature = "embed Globals.dll", Exclude = false)]

// TODO: Remove these lines once Eazfuscator.NET has out of the box support for WCF interface obfuscation
[assembly: Obfuscation(Feature = "Apply to type serviceDemo.demoService: renaming", Exclude = true)]
[assembly: Obfuscation(Feature = "Apply to type serviceDemo.IOldNetlink: renaming", Exclude = true)]

// Embed AESEncryptDecrypt.dll into the assembly
[assembly: Obfuscation(Feature = "embed AESEncryptDecrypt.dll", Exclude = false)]