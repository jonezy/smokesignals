require 'rake/clean'
require 'albacore'

SELF_PATH = File.dirname(__FILE__)
PATH_TO_MSBUILD = "C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\msbuild.exe"

# list of files and directories to clean, change to suit your liking
CLEAN.exclude("**/core","**/_sql")
CLEAN.include("*.cache", "*.xml", "*.suo", "**/obj", "**/bin", "*.nuspec", "*.nupkg")

##########################################
# CONFIGURE THIS STUFF ONLY
##########################################
$PROJECT_NAME = "Smokesignals" # this should reflect the name of your project
$VERSION_NUMBER = "1.4.1" # update this before running rake nuget:pack
$NUGET_OUTPUT_DIR = File.join(File.dirname(__FILE__), "/nuget") 
task :default => :pack

desc "packs the project based on the existing conventions"
task :pack do |t| 
  Rake::Task["clean"].invoke
  Rake::Task["assemblyinfo"].invoke
  Rake::Task["build"].invoke("Release")
  Rake::Task["spec"].invoke

  Dir.mkdir($NUGET_OUTPUT_DIR) unless File.directory?($NUGET_OUTPUT_DIR)
  
  FileUtils.copy("#{$PROJECT_NAME}/bin/release/#{$PROJECT_NAME}.dll", "#{$NUGET_OUTPUT_DIR}/lib")
  FileUtils.copy("Smokesignals.MvcExample/Public/css/messages.css", "#{$NUGET_OUTPUT_DIR}/content/Public/css")

  Dir.glob('Smokesignals.MvcExample/Public/js/*.js').each do |f|
      FileUtils.copy(f, "#{$NUGET_OUTPUT_DIR}/content/Public/js")
  end

  system("nuget pack #{$NUGET_OUTPUT_DIR}/#{$PROJECT_NAME}.nuspec -Version #{$VERSION_NUMBER} -OutputDirectory #{$NUGET_OUTPUT_DIR}")
end

desc "Generates a nuget spec file for the current project"
task :spec do |t|
  system("nuget spec -f #{$PROJECT_NAME}")
end

# builds all the .sln files in the directory
task :build, :config do |t, args| 
  desc "builds all of the .sln files in the current directory"
  config = !args.config ? "Debug" : args.config

  Dir.glob('*.sln') do |file|
    puts "\nBuilding #{file}"
    system("#{PATH_TO_MSBUILD} /v:q /p:Configuration=#{config}")
  end
end

# Builds the AssemblyInfo.cs file for the project
assemblyinfo :assemblyinfo  do |asm,args|
  puts "Writing AssemblyInfo for #{args.project}"

  asm.version = $VERSION_NUMBER 
  asm.company_name = $PROJECT_NAME
  asm.product_name = $PROJECT_NAME
  asm.title = $PROJECT_NAME
  asm.description = $PROJECT_NAME 
  asm.output_file = "#{SELF_PATH}/#{$PROJECT_NAME}/Properties/AssemblyInfo.cs"
end
