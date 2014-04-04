#-*- encoding: utf-8
require 'csv'
Encoding.default_external = 'UTF-8'

def shaping(name, sjis, utf8)
  out = File.open("./#{name}_s.csv", "w")
  File.open("./#{name}.csv") do |f|
    f.each do |row|
      row = row.split(',')
      if row.length > 2 then
        out.write(row[sjis] + "," + row[utf8] + "\n")
      end
    end
  end
end

shaping("hirakata", 1, 3)
shaping("kanji", 4, 6)
