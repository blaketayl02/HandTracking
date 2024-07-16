import cv2
import mediapipe as mp
import numpy as np
import socket
import struct

# Initialize MediaPipe solutions
mp_hands = mp.solutions.hands
hands = mp_hands.Hands(max_num_hands=2, min_detection_confidence=0.6, min_tracking_confidence=0.5)
mp_face_mesh = mp.solutions.face_mesh
face_mesh = mp_face_mesh.FaceMesh()

# Initialize webcam
cap = cv2.VideoCapture(0, cv2.CAP_DSHOW)  # Remove CAP_DSHOW if NOT on Windows
cap.set(3, 640)  # Width
cap.set(4, 480)  # Height

# Finger landmark connections (MediaPipe hand landmark indexing)
connections = [
    (0, 1), (1, 2), (2, 3), (3, 4),  # Thumb
    (0, 5), (5, 6), (6, 7), (7, 8),  # Index
    (0, 9), (9, 10), (10, 11), (11, 12),  # Middle
    (0, 13), (13, 14), (14, 15), (15, 16),  # Ring
    (0, 17), (17, 18), (18, 19), (19, 20)  # Pinky
]

fingerTips = [4, 8, 12, 16, 20]
fingerJoints = [4, 6, 10, 14, 18]

# Setup TCP Socket
HOST = '127.0.0.1'
PORT = 65535
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
sock.bind((HOST, PORT))
sock.listen(1)
print("Waiting for connection")
connection_send, client_address = sock.accept()
print(f"Connected to {client_address}")


try:
    while True:
        success, image = cap.read()
        if not success:
            break

        image = cv2.flip(image, 1)
        rgb_image = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)

        black_background = np.zeros_like(image)

        hand_results = hands.process(rgb_image)

        # Prepare to store data for both hands, initialized to zeros (meaning no hand detected)
        data_left = np.zeros(63, dtype=float)
        data_right = np.zeros(63, dtype=float)

        if hand_results.multi_hand_landmarks:
            for hand_index, hand_landmarks in enumerate(hand_results.multi_hand_landmarks):
                # Determine if the hand is left or right
                hand_label = hand_results.multi_handedness[hand_index].classification[0].label

                # Prepare the data array for the current hand
                data_hand = []
                for landmark in hand_landmarks.landmark:
                    data_hand.extend([landmark.x, landmark.y, landmark.z])

                # Assign the data to the appropriate hand
                if hand_label == 'Left':
                    data_left = np.array(data_hand)
                else:  # hand_label == 'Right'
                    data_right = np.array(data_hand)

                packed_data_left = struct.pack('f' * 63, *data_left) if np.any(data_left) else struct.pack('f' * 63, *(0 for _ in range(63)))
                packed_data_right = struct.pack('f' * 63, *data_right) if np.any(data_right) else struct.pack('f' * 63, *(0 for _ in range(63)))

                packed_data_both_hands = packed_data_left + packed_data_right

                print(f"Left Hand: {data_left}")
                print(f"Right Hand: {data_right}")

        # Here, you need to decide how you want to send both hands' data.
        # For simplicity, you could send them one after the other and have the receiving end know the order.
                connection_send.sendall(packed_data_both_hands)



                for connection in connections:
                    start_idx, end_idx = connection
                    start_landmark = hand_landmarks.landmark[start_idx]
                    end_landmark = hand_landmarks.landmark[end_idx]
                    start_point = (int(start_landmark.x * image.shape[1]), int(start_landmark.y * image.shape[0]))
                    end_point = (int(end_landmark.x * image.shape[1]), int(end_landmark.y * image.shape[0]))
                    cv2.line(black_background, start_point, end_point, (0, 255, 0), 2)

            # Draw dots for each hand joint
                for landmark in hand_landmarks.landmark:
                    x, y = int(landmark.x * image.shape[1]), int(landmark.y * image.shape[0])
                    cv2.circle(black_background, (x, y), 4, (0, 255, 0), cv2.FILLED)

        # Face detection
        face_results = face_mesh.process(rgb_image)
        if face_results.multi_face_landmarks:
            for facial_landmarks in face_results.multi_face_landmarks:
                for landmark in facial_landmarks.landmark:
                    x, y = int(landmark.x * image.shape[1]), int(landmark.y * image.shape[0])
                    cv2.circle(black_background, (x, y), 1, (0, 255, 0), cv2.FILLED)

        # Display the result
        cv2.imshow('Face and Handtracking', black_background)

        if cv2.waitKey(5) & 0xFF == 27:  # ESC to exit
            break



finally:
    cap.release()
    cv2.destroyAllWindows()
    connection_send.close()
    sock.close()
