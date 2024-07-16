import socket
import struct

# Server's IP address and port
SERVER_HOST = '127.0.0.1'
SERVER_PORT = 65535

# Create a TCP/IP socket
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

# Connect the socket to the server's address and port
server_address = (SERVER_HOST, SERVER_PORT)
print(f"Connecting to {SERVER_HOST} on port {SERVER_PORT}")
sock.connect(server_address)
print(f"Connected to {server_address}")

def recv_all(sock, n):
    data = bytearray()
    while len(data) < n:
        packet = sock.recv(n - len(data))
        if not packet:
            return None  # Connection closed, and we did not receive full data
        data.extend(packet)
    return data

# Usage within your main loop
try:
    while True:
        data = recv_all(sock, 48)  # Adjusted to read exactly 16 bytes
        if not data:
            print("No more data from server, closing connection.")
            break
        else:

            (index_x, index_y, middle_x, middle_y, ring_x, ring_y, pinky_x, pinky_y, thumb_x, thumb_y, wrist_x, wrist_y) = struct.unpack('ffffffffffff', data)
            print(f"Wrist: x={wrist_x}, y={wrist_y}; Index tip: x={index_x}, y={index_y}; middle tip: x={middle_x}, y={middle_y}; ring tip: x={ring_x}, y={ring_y};"
                  f"pinky tip: x={pinky_x}, y={pinky_y}; thumb tip: x={thumb_x}, y={thumb_y}")
except Exception as e:
    print(f"Error: {e}")
finally:
    print('Closing socket')
    sock.close()
