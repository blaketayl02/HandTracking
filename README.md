Combining Mediapipe and Unity to create real time 3D hand tracking and modeling.
Using the MediaPipe library from Google, we are able to track our hands, and face in real time via webcam input.
We can track the [X,Y,Z] coordinates of both our hands and store them in a vector. We then send the vector to Unity through a TCP Socket.
Then by assigning the coordinates to their respective joints of the 3D model, we are able to move the hand in real time!
