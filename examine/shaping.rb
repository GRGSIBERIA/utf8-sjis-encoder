#-*- encoding: utf-8
require 'csv'
Encoding.default_external = 'UTF-8'

File.open("./hirakata.csv") do |f|
  f.each do |row|
    row = row.split(',')
  end
end
