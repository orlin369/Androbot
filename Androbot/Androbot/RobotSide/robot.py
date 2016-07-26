#!/usr/bin/env python 
# -*- coding: utf-8 -*-

'''

MIT License

Copyright (c) [2016] [Orlin Dimitrov]

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

'''

__author__ = "Orlin Dimitrov"
__copyright__ = "Copyright 2016, IO Soft."
__credits__ = ["Neven Boyanov", "Delyan Spasov"]
__license__ = "MIT"
__version__ = "1.0.0"
__maintainer__ = "Orlin Dimitrov"
__email__ = "orlin369@abv.bg"
__status__ = "Debug"

# Documentation: https://www.python.org/dev/peps/pep-0008/
# Licensing:     http://choosealicense.com/licenses/
# Description:   http://stackoverflow.com/questions/1523427/what-is-the-common-header-format-of-python-files
# Doxygen:       https://www.stack.nl/~dimitri/doxygen/manual/docblocks.html#pythonblocks

import android

import os, time, socket, sys, json, bluetooth, urllib

from BaseHTTPServer import BaseHTTPRequestHandler, HTTPServer
from urlparse import urlparse

# Server parameters.
PORT_NUMBER = 8080

# Blutooth MAC adresses.
#BT_DEVICE_ID = '00:11:11:18:62:60' 
#BT_DEVICE_ID = '20:14:08:13:05:93' # DiO_ROBOT
#BT_DEVICE_ID = '98:d3:34:90:5f:5d' # TivaBot

#class RobotKarel():
#
#    def __init__(self):
#        print('')
#
#    def connect(self, btid):

# This class will handles any incoming request from the clients.
class RobotServiceHandler(BaseHTTPRequestHandler):

    # Android API instance.
    __droid = android.Android()
    
    # Bluetooh service.
    __UUID = "00001101-0000-1000-8000-00805F9B34FB"

    # PAGES
    __the_page = '<html><head></head><body>Hello, Orlin!</body></html>'
    __no_page = '<html><head></head><body>Error 404</body></html>'

    # FILE PATHS
    __cam_picture_path = '/sdcard/foo.jpg'

    # MIME Type
    __mime_type = ''

    # COMMANDS
    __ROBOT_CONNECT = 'connect'            # value: device address.
    __ROBOT_DISCONNECT = 'disconnect'    # value: null
    __ROBOT_STOP = 'robot_stop'            # value: degree
    __ROBOT_ROTATE = 'robot_rotate'        # value: degree
    __ROBOT_MOVE = 'robot_move'            # value: centimeters
    __ROBOT_CURVE = 'robot_curve'        # value: separated centimeters
    __CAPTURE = 'capture'                # value: camera index.
    __LOCATION = 'location'                # value: start, stop, get
    __TALK = 'talk'                        # value: text
    __SENSORS = 'sensors'                # value: null

    # KEYS
    __KEY = 'command'
    __VALUES = 'values'
    
    # VSSD - Vertical Separator Separated Data
    __SEPARATOR = '|'
    
    __message = ''

    # Handler for the GET requests.
    def do_GET(self):
        
        path = self.path
        parsed_url = urlparse(path)
        
        if(parsed_url.query != None and parsed_url.query != ""):
            #self.__the_page = '{0}{2}{1}'.format('0.000', '0.000', '|')
            self.__the_page = self.__execute_query(parsed_url.query)
            self.send_response(200)
        
        else:
            # Convert relative path to absolute path.
            url_path = self.__from_realative_to_absolute(parsed_url.path)
            #print 'path: ', url_path
            
            if(os.path.exists(url_path) == True):
                self.__mime_type = self.__get_mime(url_path) 
                self.__the_page = self.__get_content(url_path)
                # OK
                self.send_response(200)
            else:
                self.__the_page = self.__no_page
                # No file.
                self.send_response(404)
        
        # ============================================
        self.send_header('Content-type', self.__mime_type)
        self.end_headers()
        # Send the page
        self.wfile.write(self.__the_page)
        
        return
    
    # Translate relative path to absolute.
    def __from_realative_to_absolute(self, url_path):
        
        curent_path = os.path.dirname(os.path.realpath(__file__))
        full_path = curent_path + url_path
        full_path = full_path.replace('/', '\\')
        
        if(os.path.isdir(full_path) == True):
            full_path = os.path.join(full_path, self.__default_file_name)
        elif(os.path.isfile(full_path) == True):
            full_path = full_path
            
        return full_path
        
    # Translate to dictionary.
    def __query_to_dict(self, values):
        values = urllib.unquote(values).decode('utf8')
        split_values = values.split('&')
        dict = {}
        for pear in split_values:
            split_pear = pear.split('=')
            if((split_pear != None) and (len(split_pear) == 2)):
                dict[split_pear[0]] = split_pear[1]
                
        return dict
        
    # Get the content from the file.
    def __get_content(self, file_path):
        content = ''
        # Open the file.
        page_file = open(file_path, 'rb')
        # Read page file content.
        content = page_file.read()
        # Close the content.
        page_file.close()
        
        return content
        
    # Returns MIME type.
    def __get_mime(self, url_path):
        split_string = url_path.split('.')
        extention = ''
        mime_type = ''
        split_count = len(split_string)
        if(split_count >= 2):
            extention = split_string[split_count - 1]
            if(extention == 'css'):
                mime_type = "text/css";
            elif(extention == "jpg"):
                mime_type = "image/jpeg";
            elif(extention == "jpeg"):
                mime_type = "image/jpeg";
            elif(extention == "png"):
                mime_type = "image/png";
            elif(extention == "bmp"):
                mime_type = "image/bmp";
            elif(extention == "gif"):
                mime_type = "image/gif";
            elif(extention == "emf"):
                mime_type = "image/emf";
            elif(extention == "ico"):
                mime_type = "image/ico";
            elif(extention == "csv"):
                mime_type = "text/csv";
            elif(extention == "html"):
                mime_type = "text/html";
            elif(extention == "js"):
                mime_type = "text/javascript";
            else:
                mime_type = "text/html";
            
        return mime_type
        
    # Execute query from the request.
    def __execute_query(self, url_query):

        dict = self.__query_to_dict(url_query)
        key = dict[self.__KEY]
        values = dict[self.__VALUES]
        result = ''

        # http://127.0.0.1:8080/?command=connect&values=00:11:11:18:62:60
        if(key == self.__ROBOT_CONNECT):
            #if(self.__droid.checkBluetoothState() == False):
            self.__droid.toggleBluetoothState(True)
            self.__droid.bluetoothConnect(self.__UUID, values)
            
        # http://127.0.0.1:8080/?command=disconnect&values=null
        elif(key == self.__ROBOT_DISCONNECT):
            self.__droid.bluetoothStop()
            
        # http://127.0.0.1:8080/?command=robot_stop&values=null
        elif(key == self.__ROBOT_STOP):
            self.__droid.bluetoothWrite("STOP")
            
        # http://127.0.0.1:8080/?command=robot_move&values=100.0|150.0
        elif(key == self.__ROBOT_MOVE):
            dist, speed = values.split(self.__SEPARATOR)
            # String.Format("?M{0}{1:D4}", (value > 0) ? "+" : "", value)
            #'{0}, {1}, {2}'.format('a', 'b', 'c')
            #str.zfill(40)
            #dist = 42
            # ?LF255RB255\n
            command = '?L{0}{1}R{0}{1}\n'.format('F' if dist > 0 else 'B', str(dist).zfill(4))
            print(command)
            self.__droid.bluetoothWrite(command)
            
        # http://127.0.0.1:8080/?command=robot_rotate&values=100.0|150.0
        elif(key == self.__ROBOT_ROTATE):
            #deg, speed = values.split(self.__SEPARATOR)
            self.__droid.bluetoothWrite("ROTATE")
            
        # http://127.0.0.1:8080/?command=robot_curve&values=10.0|15.0|10.0|15.0
        elif(key == self.__ROBOT_CURVE):
            #lDist, lSpeed, rDist, rSpeed = values.split(self.__SEPARATOR)
            self.__droid.bluetoothWrite("CURVE" + values)
            
        # http://127.0.0.1:8080/?command=capture&values=0
        elif(key == self.__CAPTURE):
            self.__droid.cameraCapturePicture(self.__cam_picture_path)
            image = self.__get_content(self.__cam_picture_path);
            result = image
            
        # http://127.0.0.1:8080/?command=location&values=start
        elif(key == self.__LOCATION):
            if(values == 'start'):
                self.__droid.startLocating()
            
            elif(values == 'stop'):
                self.__droid.stopLocating()
                
            elif(values == 'get'):
                location = self.__droid.readLocation().result
                
                if(location == {}):
                    location = getLastKnownLocation().result
                
                if(location != {}):
                    result = location

                result = json.dumps(result)
        
        # http://127.0.0.1:8080/?command=talk&values=text
        elif(key == self.__TALK):            
            if(values != ''):
                self.__droid.ttsSpeak(values)

        # http://127.0.0.1:8080/?command=sensors&values=null
        elif(key == self.__SENSORS):
            if(values == 'start'):
                self.__droid.startSensingTimed(1, 10)

            elif(values == 'stop'):
                self.__droid.stopSensing()

            elif(values == 'get'):
                time.sleep(1)
                sensor_data = self.__droid.readSensors().result
                result = json.dumps(sensor_data, ensure_ascii=False)

        # Print the whole message contetnt.
        self.__message = 'Message: {0}, {1}'.format(key, values)
        print(self.__message)
        
        return result
        
# Main function.
def main():
    global PORT_NUMBER

    # Try to run ...
    try:
        # Create a web server and define the handler
        # to manage the incoming request.
        server = HTTPServer(('', PORT_NUMBER), RobotServiceHandler)
        
        # Print network addresses.
        ip_address = socket.gethostbyname(socket.gethostname())
        schema = 'Started server @ {0}:{1}'.format(ip_address, PORT_NUMBER)
        print(schema)

        # Wait forever for incoming http requests.
        server.serve_forever()
        
    except TypeError:
        print('Type error ...')
        
    except KeyboardInterrupt:
        print('^C received, shutting down the server')
        server.socket.close()
        
# Where all begin ...
if(__name__ == '__main__'):
    main()
