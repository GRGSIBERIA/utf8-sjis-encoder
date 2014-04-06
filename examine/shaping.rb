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

def writecs(out, path, key, val)
  File.open("./#{path}_s.csv") do |f|
    f.each do |row|
      row = row.chomp.split(',')
      unless row.include?("------") then
        out.write("#{row[key].hex}, #{row[val].hex}\n")
      end
    end
  end
end

def makecs(path, key, val)
  out = File.open("./#{path}.cs", "w")
  writecs(out, "hirakata", key, val)
  writecs(out, "kanji", key, val)
end

def toarray(path)
  out = File.open("./#{path}_a.cs", "w")
  hash = Hash.new
  File.open("./#{path}.cs") do |f|
    f.each do |row|
      row = row.chomp.split(',')
      a = row[0].hex
      b = row[1].hex
      hash[a] = b
    end
  end

  for i in 0..65536 do 
    buf = hash[i]
    if buf == nil then
      out.write("0,")
    else
      out.write("#{buf},")
    end
  end
end

shaping("hirakata", 1, 4)
shaping("kanji", 4, 7)

makecs("to_utf8", 0, 1)
makecs("to_sjis", 1, 0)

#toarray("to_utf8")
#toarray("to_sjis")