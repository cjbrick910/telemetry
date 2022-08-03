import serial
import termios

port = '/dev/ttyUSB0'
f = open(port)

attrs = termios.tcgetattr(f)
attrs[2] = attrs[2] & ~termios.HUPCL
termios.tcsetattr(f, termios.TCSAFLUSH, attrs)
f.close()

ser = serial.Serial()

ser.baudrate = 9600
ser.port = port
print ('dtr = ', ser.dtr)
ser.open()
ser.readline()

