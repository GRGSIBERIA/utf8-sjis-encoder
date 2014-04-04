#-*- encoding: utf-8
require 'csv'
Encoding.default_external = 'UTF-8'

def shaping(name, sjis, utf8)
  out = File.open("./#{name}_s.csv", "w")
  File.open("./#{name}.csv") do |f|
    f.each do |row|
      row = row.split(',')
      unless row.include?("SJIS")
        if row.length > 2 then
          out.write(row[sjis] + "," + row[utf8] + "\n")
        end
      end
    end
  end
end

def makecs()
  out = File.open("./to_sjis.cs", "w")
  File.open("./hirakata_s.csv") do |f|
    f.each do |row|
      row = row.chomp.split(',')
      out.write("\{0x#{row[0]}, 0x#{row[1]}\},\n")
    end
  end
end

shaping("hirakata", 1, 3)
shaping("kanji", 4, 6)

makecs