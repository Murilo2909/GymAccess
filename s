[1mdiff --git a/API/Program.cs b/API/Program.cs[m
[1mindex 1f78af2..5721c14 100644[m
[1m--- a/API/Program.cs[m
[1m+++ b/API/Program.cs[m
[36m@@ -30,11 +30,14 @@[m [mbuilder.Services.AddEndpointsApiExplorer();[m
 builder.Services.AddSwaggerGen(c =>[m
 {[m
     c.SwaggerDoc("v1", new OpenApiInfo[m
[31m-    { [m
[32m+[m[32m    {[m
         Title = "GymAccess API",[m
         Version = "v1"[m
     });[m
 [m
[32m+[m[32m    // evita conflitos de nome[m
[32m+[m[32m    c.CustomSchemaIds(type => type.FullName);[m
[32m+[m
     c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme[m
     {[m
         Name = "Authorization",[m
[36m@@ -47,7 +50,7 @@[m [mbuilder.Services.AddSwaggerGen(c =>[m
 [m
     c.AddSecurityRequirement(new OpenApiSecurityRequirement[m
     {[m
[31m-        { [m
[32m+[m[32m        {[m
             new OpenApiSecurityScheme[m
             {[m
                 Reference = new OpenApiReference[m
[36m@@ -60,7 +63,6 @@[m [mbuilder.Services.AddSwaggerGen(c =>[m
         }[m
     });[m
 });[m
[31m-[m
 // 🔒 Autenticação JWT[m
 builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)[m
     .AddJwtBearer(options =>[m
[36m@@ -100,11 +102,8 @@[m [mbuilder.Services.AddAuthorization();[m
 var app = builder.Build();[m
 [m
 // Swagger no Dev[m
[31m-if (app.Environment.IsDevelopment())[m
[31m-{[m
[31m-    app.UseSwagger();[m
[31m-    app.UseSwaggerUI();[m
[31m-}[m
[32m+[m[32mapp.UseSwagger();[m
[32m+[m[32mapp.UseSwaggerUI();[m
 [m
 app.UseHttpsRedirection();[m
 app.UseAuthentication();[m
[1mdiff --git a/API/bin/Debug/net10.0/DB.dll b/API/bin/Debug/net10.0/DB.dll[m
[1mindex 1d84aeb..a7c9e8f 100644[m
Binary files a/API/bin/Debug/net10.0/DB.dll and b/API/bin/Debug/net10.0/DB.dll differ
[1mdiff --git a/API/bin/Debug/net10.0/DB.pdb b/API/bin/Debug/net10.0/DB.pdb[m
[1mindex 0086277..33a9f61 100644[m
Binary files a/API/bin/Debug/net10.0/DB.pdb and b/API/bin/Debug/net10.0/DB.pdb differ
[1mdiff --git a/API/bin/Debug/net10.0/GymAccess.dll b/API/bin/Debug/net10.0/GymAccess.dll[m
[1mindex cd6386b..82de381 100644[m
Binary files a/API/bin/Debug/net10.0/GymAccess.dll and b/API/bin/Debug/net10.0/GymAccess.dll differ
[1mdiff --git a/API/bin/Debug/net10.0/GymAccess.exe b/API/bin/Debug/net10.0/GymAccess.exe[m
[1mindex d4d3ddd..81b85c1 100644[m
Binary files a/API/bin/Debug/net10.0/GymAccess.exe and b/API/bin/Debug/net10.0/GymAccess.exe differ
[1mdiff --git a/API/bin/Debug/net10.0/GymAccess.pdb b/API/bin/Debug/net10.0/GymAccess.pdb[m
[1mindex d77877c..6d05c8c 100644[m
Binary files a/API/bin/Debug/net10.0/GymAccess.pdb and b/API/bin/Debug/net10.0/GymAccess.pdb differ
[1mdiff --git a/API/bin/Debug/net10.0/GymAccess.staticwebassets.endpoints.json b/API/bin/Debug/net10.0/GymAccess.staticwebassets.endpoints.json[m
[1mindex df0eabb..fd19b63 100644[m
[1m--- a/API/bin/Debug/net10.0/GymAccess.staticwebassets.endpoints.json[m
[1m+++ b/API/bin/Debug/net10.0/GymAccess.staticwebassets.endpoints.json[m
[36m@@ -1 +1 @@[m
[31m-{"Version":1,"ManifestType":"Build","Endpoints":[{"Route":"images/members/17.70jlngu3el.jpg","AssetFile":"images/members/17.jpg","Selectors":[],"ResponseHeaders":[{"Name":"Cache-Control","Value":"max-age=31536000, immutable"},{"Name":"Content-Length","Value":"4983"},{"Name":"Content-Type","Value":"image/jpeg"},{"Name":"ETag","Value":"\"iVWoChGjl2feLXs/I3UikS7s0wKMuTi2O/A6soY4+R4=\""},{"Name":"Last-Modified","Value":"Wed, 26 Nov 2025 16:11:26 GMT"}],"EndpointProperties":[{"Name":"fingerprint","Value":"70jlngu3el"},{"Name":"integrity","Value":"sha256-iVWoChGjl2feLXs/I3UikS7s0wKMuTi2O/A6soY4+R4="},{"Name":"label","Value":"images/members/17.jpg"}]},{"Route":"images/members/17.jpg","AssetFile":"images/members/17.jpg","Selectors":[],"ResponseHeaders":[{"Name":"Cache-Control","Value":"max-age=3600, must-revalidate"},{"Name":"Content-Length","Value":"4983"},{"Name":"Content-Type","Value":"image/jpeg"},{"Name":"ETag","Value":"\"iVWoChGjl2feLXs/I3UikS7s0wKMuTi2O/A6soY4+R4=\""},{"Name":"Last-Modified","Value":"Wed, 26 Nov 2025 16:11:26 GMT"}],"EndpointProperties":[{"Name":"integrity","Value":"sha256-iVWoChGjl2feLXs/I3UikS7s0wKMuTi2O/A6soY4+R4="}]},{"Route":"images/members/18.70jlngu3el.jpg","AssetFile":"images/members/18.jpg","Selectors":[],"ResponseHeaders":[{"Name":"Cache-Control","Value":"max-age=31536000, immutable"},{"Name":"Content-Length","Value":"4983"},{"Name":"Content-Type","Value":"image/jpeg"},{"Name":"ETag","Value":"\"iVWoChGjl2feLXs/I3UikS7s0wKMuTi2O/A6soY4+R4=\""},{"Name":"Last-Modified","Value":"Wed, 26 Nov 2025 16:11:50 GMT"}],"EndpointProperties":[{"Name":"fingerprint","Value":"70jlngu3el"},{"Name":"integrity","Value":"sha256-iVWoChGjl2feLXs/I3UikS7s0wKMuTi2O/A6soY4+R4="},{"Name":"label","Value":"images/members/18.jpg"}]},{"Route":"images/members/18.jpg","AssetFile":"images/members/18.jpg","Selectors":[],"ResponseHeaders":[{"Name":"Cache-Control","Value":"max-age=3600, must-revalidate"},{"Name":"Content-Length","Value":"4983"},{"Name":"Content-Type","Value":"image/jpeg"},{"Name":"ETag","Value":"\"iVWoChGjl2feLXs/I3UikS7s0wKMuTi2O/A6soY4+R4=\""},{"Name":"Last-Modified","Value":"Wed, 26 Nov 2025 16:11:50 GMT"}],"EndpointProperties":[{"Name":"integrity","Value":"sha256-iVWoChGjl2feLXs/I3UikS7s0wKMuTi2O/A6soY4+R4="}]},{"Route":"images/members/19.9ragzj0w8n.jpg","AssetFile":"images/members/19.jpg","Selectors":[],"ResponseHeaders":[{"Name":"Cache-Control","Value":"max-age=31536000, immutable"},{"Name":"Content-Length","Value":"25682"},{"Name":"Content-Type","Value":"image/jpeg"},{"Name":"ETag","Value":"\"izZdadDKHlyMtSQsGDU7Xd0GUvWPoHw+Tbf81tOZipw=\""},{"Name":"Last-Modified","Value":"Wed, 26 Nov 2025 17:16:12 GMT"}],"EndpointProperties":[{"Name":"fingerprint","Value":"9ragzj0w8n"},{"Name":"integrity","Value":"sha256-izZdadDKHlyMtSQsGDU7Xd0GUvWPoHw+Tbf81tOZipw="},{"Name":"label","Value":"images/members/19.jpg"}]},{"Route":"images/members/19.jpg","AssetFile":"images/members/19.jpg","Selectors":[],"ResponseHeaders":[{"Name":"Cache-Control","Value":"max-age=3600, must-revalidate"},{"Name":"Content-Length","Value":"25682"},{"Name":"Content-Type","Value":"image/jpeg"},{"Name":"ETag","Value":"\"izZdadDKHlyMtSQsGDU7Xd0GUvWPoHw+Tbf81tOZipw=\""},{"Name":"Last-Modified","Value":"Wed, 26 Nov 2025 17:16:12 GMT"}],"EndpointProperties":[{"Name":"integrity","Value":"sha256-izZdadDKHlyMtSQsGDU7Xd0GUvWPoHw+Tbf81tOZipw="}]},{"Route":"images/members/20.9ragzj0w8n.jpg","AssetFile":"images/members/20.jpg","Selectors":[],"ResponseHeaders":[{"Name":"Cache-Control","Value":"max-age=31536000, immutable"},{"Name":"Content-Length","Value":"25682"},{"Name":"Content-Type","Value":"image/jpeg"},{"Name":"ETag","Value":"\"izZdadDKHlyMtSQsGDU7Xd0GUvWPoHw+Tbf81tOZipw=\""},{"Name":"Last-Modified","Value":"Wed, 26 Nov 2025 17:19:33 GMT"}],"EndpointProperties":[{"Name":"fingerprint","Value":"9ragzj0w8n"},{"Name":"integrity","Value":"sha256-izZdadDKHlyMtSQsGDU7Xd0GUvWPoHw+Tbf81tOZipw="},{"Name":"label","Value":"images/members/20.jpg"}]},{"Route":"images/members/20.jpg","AssetFile":"images/members/20.jpg","Selectors":[],"ResponseHeaders":[{"Name":"Cache-Control","Value":"max-age=3600, must-revalidate"},{"Name":"Content-Length","Value":"25682"},{"Name":"Content-Type","Value":"image/jpeg"},{"Name":"ETag","Value":"\"izZdadDKHlyMtSQsGDU7Xd0GUvWPoHw+Tbf81tOZipw=\""},{"Name":"Last-Modified","Value":"Wed, 26 Nov 2025 17:19:33 GMT"}],"EndpointProperties":[{"Name":"integrity","Value":"sha256-izZdadDKHlyMtSQsGDU7Xd0GUvWPoHw+Tbf81tOZipw="}]}]}[m
\ No newline at end of file[m
[32m+[m[32m{"Version":1,"ManifestType":"Build","Endpoints":[{"Route":"images/members/17.70jlngu3el.jpg","AssetFile":"images/members/17.jpg","Selectors":[],"ResponseHeaders":[{"Name":"Cache-Control","Value":"max-age=31536000, immutable"},{"Name":"Content-Length","Value":"4983"},{"Name":"Content-Type","Value":"image/jpeg"},{"Name":"ETag","Value":"\"iVWoChGjl2feLXs/I3UikS7s0wKMuTi2O/A6soY4+R4=\""},{"Name":"Last-Modified","Value":"Wed, 26 Nov 2025 16:11:26 GMT"}],"EndpointProperties":[{"Name":"fingerprint","Value":"70jlngu3el"},{"Name":"integrity","Value":"sha256-iVWoChGjl2feLXs/I3UikS7s0wKMuTi2O/A6soY4+R4="},{"Name":"label","Value":"images/members/17.jpg"}]},{"Route":"images/members/17.jpg","AssetFile":"images/members/17.jpg","Selectors":[],"ResponseHeaders":[{"Name":"Cache-Control","Value":"max-age=3600, must-revalidate"},{"Name":"Content-Length","Value":"4983"},{"Name":"Content-Type","Value":"image/jpeg"},{"Name":"ETag","Value":"\"iVWoChGjl2feLXs/I3UikS7s0wKMuTi2O/A6soY4+R4=\""},{"Name":"Last-Modified","Value":"Wed, 26 Nov 2025 16:11:26 GMT"}],"EndpointProperties":[{"Name":"integrity","Value":"sha256-iVWoChGjl2feLXs/I3UikS7s0wKMuTi2O/A6soY4+R4="}]},{"Route":"images/members/18.70jlngu3el.jpg","AssetFile":"images/members/18.jpg","Selectors":[],"ResponseHeaders":[{"Name":"Cache-Control","Value":"max-age=31536000, immutable"},{"Name":"Content-Length","Value":"4983"},{"Name":"Content-Type","Value":"image/jpeg"},{"Name":"ETag","Value":"\"iVWoChGjl2feLXs/I3UikS7s0wKMuTi2O/A6soY4+R4=\""},{"Name":"Last-Modified","Value":"Wed, 26 Nov 2025 16:11:50 GMT"}],"EndpointProperties":[{"Name":"fingerprint","Value":"70jlngu3el"},{"Name":"integrity","Value":"sha256-iVWoChGjl2feLXs/I3UikS7s0wKMuTi2O/A6soY4+R4="},{"Name":"label","Value":"images/members/18.jpg"}]},{"Route":"images/members/18.jpg","AssetFile":"images/members/18.jpg","Selectors":[],"ResponseHeaders":[{"Name":"Cache-Control","Value":"max-age=3600, must-revalidate"},{"Name":"Content-Length","Value":"4983"},{"Name":"Content-Type","Value":"image/jpeg"},{"Name":"ETag","Value":"\"iVWoChGjl2feLXs/I3UikS7s0wKMuTi2O/A6soY4+R4=\""},{"Name":"Last-Modified","Value":"Wed, 26 Nov 2025 16:11:50 GMT"}],"EndpointProperties":[{"Name":"integrity","Value":"sha256-iVWoChGjl2feLXs/I3UikS7s0wKMuTi2O/A6soY4+R4="}]},{"Route":"images/members/19.9ragzj0w8n.jpg","AssetFile":"images/members/19.jpg","Selectors":[],"ResponseHeaders":[{"Name":"Cache-Control","Value":"max-age=31536000, immutable"},{"Name":"Content-Length","Value":"25682"},{"Name":"Content-Type","Value":"image/jpeg"},{"Name":"ETag","Value":"\"izZdadDKHlyMtSQsGDU7Xd0GUvWPoHw+Tbf81tOZipw=\""},{"Name":"Last-Modified","Value":"Wed, 26 Nov 2025 17:16:12 GMT"}],"EndpointProperties":[{"Name":"fingerprint","Value":"9ragzj0w8n"},{"Name":"integrity","Value":"sha256-izZdadDKHlyMtSQsGDU7Xd0GUvWPoHw+Tbf81tOZipw="},{"Name":"label","Value":"images/members/19.jpg"}]},{"Route":"images/members/19.jpg","AssetFile":"images/members/19.jpg","Selectors":[],"ResponseHeaders":[{"Name":"Cache-Control","Value":"max-age=3600, must-revalidate"},{"Name":"Content-Length","Value":"25682"},{"Name":"Content-Type","Value":"image/jpeg"},{"Name":"ETag","Value":"\"izZdadDKHlyMtSQsGDU7Xd0GUvWPoHw+Tbf81tOZipw=\""},{"Name":"Last-Modified","Value":"Wed, 26 Nov 2025 17:16:12 GMT"}],"EndpointProperties":[{"Name":"integrity","Value":"sha256-izZdadDKHlyMtSQsGDU7Xd0GUvWPoHw+Tbf81tOZipw="}]},{"Route":"images/members/20.9ragzj0w8n.jpg","AssetFile":"images/members/20.jpg","Selectors":[],"ResponseHeaders":[{"Name":"Cache-Control","Value":"max-age=31536000, immutable"},{"Name":"Content-Length","Value":"25682"},{"Name":"Content-Type","Value":"image/jpeg"},{"Name":"ETag","Value":"\"izZdadDKHlyMtSQsGDU7Xd0GUvWPoHw+Tbf81tOZipw=\""},{"Name":"Last-Modified","Value":"Wed, 26 Nov 2025 17:19:33 GMT"}],"EndpointProperties":[{"Name":"fingerprint","Value":"9ragzj0w8n"},{"Name":"integrity","Value":"sha256-izZdadDKHlyMtSQsGDU7Xd0GUvWPoHw+Tbf81tOZipw="},{"Name":"label","Value":"images/members/20.jpg"}]},{"Route":"images/members/20.jpg","AssetFile":"images/members/20.jpg","Selectors":[],"ResponseHeaders":[{"Name":"Cache-Control","Value":"max-age=3600, must-revalidate"},{"Name":"Content-Length","Value":"25682"},{"Name":"Content-Type","Value":"image/jpeg"},{"Name":"ETag","Value":"\"izZdadDKHlyMtSQsGDU7Xd0GUvWPoHw+Tbf81tOZipw=\""},{"Name":"Last-Modified","Value":"Wed, 26 Nov 2025 17:19:33 GMT"}],"EndpointProperties":[{"Name":"integrity","Value":"sha256-izZdadDKHlyMtSQsGDU7Xd0GUvWPoHw+Tbf81tOZipw="}]},{"Route":"images/members/21.jpg","AssetFile":"images/members/21.jpg","Selectors":[],"ResponseHeaders":[{"Name":"Cache-Control","Value":"max-age=3600, must-revalidate"},{"Name":"Content-Length","Value":"25201"},{"Name":"Content-Type","Value":"image/jpeg"},{"Name":"ETag","Value":"\"xE2a9OWs4TTvr0rP6bVrU5FHqG6ILYGGZP+A9xfVSPY=\""},{"Name":"Last-Modified","Value":"Wed, 26 Nov 2025 18:02:33 GMT"}],"EndpointProperties":[{"Name":"integrity","Value":"sha256-xE2a9OWs4TTvr0rP6bVrU5FHqG6ILYGGZP+A9xfVSPY="}]},{"Route":"images/members/21.otpuw580me.jpg","AssetFile":"images/members/21.jpg","Selectors":[],"ResponseHeaders":[{"Name":"Cache-Control","Value":"max-age=31536000, immutable"},{"Name":"Content-Length","Value":"25201"},{"Name":"Content-Type","Value":"image/jpeg"},{"Name":"ETag","Value":"\"xE2a9OWs4TTvr0rP6bVrU5FHqG6ILYGGZP+A9xfVSPY=\""},{"Name":"Last-Modified","Value":"Wed, 26 Nov 2025 18:02:33 GMT"}],"EndpointProperties":[{"Name":"fingerprint","Value":"otpuw580me"},{"Name":"integrity","Value":"sha256-xE2a9OWs4TTvr0rP6bVrU5FHqG6ILYGGZP+A9xfVSPY="},{"Name":"label","Value":"images/members/21.jpg"}]}]}[m
\ No newline at end of file[m
[1mdiff --git a/API/bin/Debug/net10.0/GymAccess.staticwebassets.runtime.json b/API/bin/Debug/net10.0/GymAccess.staticwebassets.runtime.json[m
[1mindex fc66e27..d1571bc 100644[m
[1m--- a/API/bin/Debug/net10.0/GymAccess.staticwebassets.runtime.json[m
[1m+++ b/API/bin/Debug/net10.0/GymAccess.staticwebassets.runtime.json[m
[36m@@ -1 +1 @@[m
[31m-{"ContentRoots":["C:\\Projetos\\GymAccess\\API\\wwwroot\\"],"Root":{"Children":{"images":{"Children":{"members":{"Children":{"17.jpg":{"Children":null,"Asset":{"ContentRootIndex":0,"SubPath":"images/members/17.jpg"},"Patterns":null},"18.jpg":{"Children":null,"Asset":{"ContentRootIndex":0,"SubPath":"images/members/18.jpg"},"Patterns":null},"19.jpg":{"Children":null,"Asset":{"ContentRootIndex":0,"SubPath":"images/members/19.jpg"},"Patterns":null},"20.jpg":{"Children":null,"Asset":{"ContentRootIndex":0,"SubPath":"images/members/20.jpg"},"Patterns":null}},"Asset":null,"Patterns":null}},"Asset":null,"Patterns":null}},"Asset":null,"Patterns":[{"ContentRootIndex":0,"Pattern":"**","Depth":0}]}}[m
\ No newline at end of file[m
[32m+[m[32m{"ContentRoots":["C:\\Projetos\\GymAccess\\API\\wwwroot\\"],"Root":{"Children":{"images":{"Children":{"members":{"Children":{"17.jpg":{"Children":null,"Asset":{"ContentRootIndex":0,"SubPath":"images/members/17.jpg"},"Patterns":null},"18.jpg":{"Children":null,"Asset":{"ContentRootIndex":0,"SubPath":"images/members/18.jpg"},"Patterns":null},"19.jpg":{"Children":null,"Asset":{"ContentRootIndex":0,"SubPath":"images/members/19.jpg"},"Patterns":null},"20.jpg":{"Children":null,"Asset":{"ContentRootIndex":0,"SubPath":"images/members/20.jpg"},"Patterns":null},"21.jpg":{"Children":null,"Asset":{"ContentRootIndex":0,"SubPath":"images/members/21.jpg"},"Patterns":null}},"Asset":null,"Patterns":null}},"Asset":null,"Patterns":null}},"Asset":null,"Patterns":[{"ContentRootIndex":0,"Pattern":"**","Depth":0}]}}[m
\ No newline at end of file[m
[1mdiff --git a/API/obj/Debug/net10.0/GymAccess.AssemblyInfo.cs b/API/obj/Debug/net10.0/GymAccess.AssemblyInfo.cs[m
[1mindex 1f87172..425be8c 100644[m
[1m--- a/API/obj/Debug/net10.0/GymAccess.AssemblyInfo.cs[m
[1m+++ b/API/obj/Debug/net10.0/GymAccess.AssemblyInfo.cs[m
[36m@@ -13,7 +13,7 @@[m [musing System.Reflection;[m
 [assembly: System.Reflection.AssemblyCompanyAttribute("GymAccess")][m
 [assembly: System.Reflection.AssemblyConfigurationAttribute("Debug")][m
 [assembly: System.Reflection.AssemblyFileVersionAttribute("1.0.0.0")][m
[31m-[assembly: System.Reflection.AssemblyInformationalVersionAttribute("1.0.0+5fc9c96cea569fec6cd5f4248f30656c2374863b")][m
[32m+[m[32m[assembly: System.Reflection.AssemblyInformationalVersionAttribute("1.0.0+e9b570df36c5efd376bc655cf85b240c4e928b45")][m
 [assembly: System.Reflection.AssemblyProductAttribute("GymAccess")][m
 [assembly: System.Reflection.AssemblyTitleAttribute("GymAccess")][m
 [assembly: System.Reflection.AssemblyVersionAttribute("1.0.0.0")][m
[1mdiff --git a/API/obj/Debug/net10.0/GymAccess.AssemblyInfoInputs.cache b/API/obj/Debug/net10.0/GymAccess.AssemblyInfoInputs.cache[m
[1mindex 0572b53..8605b70 100644[m
[1m--- a/API/obj/Debug/net10.0/GymAccess.AssemblyInfoInputs.cache[m
[1m+++ b/API/obj/Debug/net10.0/GymAccess.AssemblyInfoInputs.cache[m
[36m@@ -1 +1 @@[m
[31m-98ca5b9c17cdcb35c656d69657094d5418d5d1501358d6e22234aaa59a523adc[m
[32m+[m[32mea36361d50c78b02d5ed21655ab28e56e05427ac3e87f5e76223ab6c30393af0[m
[1mdiff --git a/API/obj/Debug/net10.0/GymAccess.csproj.AssemblyReference.cache b/API/obj/Debug/net10.0/GymAccess.csproj.AssemblyReference.cache[m
[1mindex 7827fd5..a1847bc 100644[m
Binary files a/API/obj/Debug/net10.0/GymAccess.csproj.AssemblyReference.cache and b/API/obj/Debug/net10.0/GymAccess.csproj.AssemblyReference.cache differ
[1mdiff --git a/API/obj/Debug/net10.0/GymAccess.dll b/API/obj/Debug/net10.0/GymAccess.dll[m
[1mindex cd6386b..82de381 100644[m
Binary files a/API/obj/Debug/net10.0/GymAccess.dll and b/API/obj/Debug/net10.0/GymAccess.dll differ
[1mdiff --git a/API/obj/Debug/net10.0/GymAccess.pdb b/API/obj/Debug/net10.0/GymAccess.pdb[m
[1mindex d77877c..6d05c8c 100644[m
Binary files a/API/obj/Debug/net10.0/GymAccess.pdb and b/API/obj/Debug/net10.0/GymAccess.pdb differ
[1mdiff --git a/API/obj/Debug/net10.0/GymAccess.sourcelink.json b/API/obj/Debug/net10.0/GymAccess.sourcelink.json[m
[1mindex 5714d19..b64a08a 100644[m
[1m--- a/API/obj/Debug/net10.0/GymAccess.sourcelink.json[m
[1m+++ b/API/obj/Debug/net10.0/GymAccess.sourcelink.json[m
[36m@@ -1 +1 @@[m
[31m-{"documents":{"C:\\Projetos\\GymAccess\\*":"https://raw.githubusercontent.com/Murilo2909/GymAccess/6d9f92bdb781487b5a7fa21097bd7756f456c45c/*"}}[m
\ No newline at end of file[m
[32m+[m[32m{"documents":{"C:\\Projetos\\GymAccess\\*":"https://raw.githubusercontent.com/Murilo2909/GymAccess/e9b570df36c5efd376bc655cf85b240c4e928b45/*"}}[m
\ No newline at end of file[m
[1mdiff --git a/API/obj/Debug/net10.0/apphost.exe b/API/obj/Debug/net10.0/apphost.exe[m
[1mindex d4d3ddd..81b85c1 100644[m
Binary files a/API/obj/Debug/net10.0/apphost.exe and b/API/obj/Debug/net10.0/apphost.exe differ
[1mdiff --git a/API/obj/Debug/net10.0/ref/GymAccess.dll b/API/obj/Debug/net10.0/ref/GymAccess.dll[m
[1mindex 53ae1b7..ffacc52 100644[m
Binary files a/API/obj/Debug/net10.0/ref/GymAccess.dll and b/API/obj/Debug/net10.0/ref/GymAccess.dll differ
[1mdiff --git a/API/obj/Debug/net10.0/refint/GymAccess.dll b/API/obj/Debug/net10.0/refint/GymAccess.dll[m
[1mindex 53ae1b7..ffacc52 100644[m
Binary files a/API/obj/Debug/net10.0/refint/GymAccess.dll and b/API/obj/Debug/net10.0/refint/GymAccess.dll differ
[1mdiff --git a/API/obj/Debug/net10.0/rjimswa.dswa.cache.json b/API/obj/Debug/net10.0/rjimswa.dswa.cache.json[m
[1mindex a9e1e1e..b86a6af 100644[m
[1m--- a/API/obj/Debug/net10.0/rjimswa.dswa.cache.json[m
[1m+++ b/API/obj/Debug/net10.0/rjimswa.dswa.cache.json[m
[36m@@ -1 +1 @@[m
[31m-{"GlobalPropertiesHash":"ieUJQ7TDBydEkKbNt1miCvinQCA21urLFO/jOdfUTxA=","FingerprintPatternsHash":"8ZRc1sGeVrPBx4lD717BgRaQekyh78QKV9SKsdt638U=","PropertyOverridesHash":"R7Rea/YQmcweqCbKffD9oUelggfpJQX85r65aYZsas0=","InputHashes":["Ja4a6SffCuJR6umhV4BKVba\u002BBJcbY5gVLB4XwQ2Qe8M=","z32LlqMHyT6eK\u002BZwW/dcEmB48x0OBzI47LUvqp/fU6M=","CZjRGOoWB27nqItmKTty/8KRYqRuJ/CzuQAvXC3JfJA=","aMPxdR2B1mkp8tjc9HB5ODmZ28hMP\u002BcyhbDv54D9HxM="],"CachedAssets":{},"CachedCopyCandidates":{}}[m
\ No newline at end of file[m
[32m+[m[32m{"GlobalPropertiesHash":"ieUJQ7TDBydEkKbNt1miCvinQCA21urLFO/jOdfUTxA=","FingerprintPatternsHash":"8ZRc1sGeVrPBx4lD717BgRaQekyh78QKV9SKsdt638U=","PropertyOverridesHash":"R7Rea/YQmcweqCbKffD9oUelggfpJQX85r65aYZsas0=","InputHashes":["Ja4a6SffCuJR6umhV4BKVba\u002BBJcbY5gVLB4XwQ2Qe8M=","z32LlqMHyT6eK\u002BZwW/dcEmB48x0OBzI47LUvqp/fU6M=","CZjRGOoWB27nqItmKTty/8KRYqRuJ/CzuQAvXC3JfJA=","aMPxdR2B1mkp8tjc9HB5ODmZ28hMP\u002BcyhbDv54D9HxM=","oNKvUo944cjnqtMEySAY4S\u002BTY1E5fwDGjKWrdkAjWxE="],"CachedAssets":{},"CachedCopyCandidates":{}}[m
\ No newline at end of file[m
[1mdiff --git a/API/obj/Debug/net10.0/rjsmcshtml.dswa.cache.json b/API/obj/Debug/net10.0/rjsmcshtml.dswa.cache.json[m
[1mindex 798cee0..3251215 100644[m
[1m--- a/API/obj/Debug/net10.0/rjsmcsh