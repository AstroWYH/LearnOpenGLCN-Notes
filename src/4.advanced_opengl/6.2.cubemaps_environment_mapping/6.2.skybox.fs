#version 330 core
out vec4 FragColor;

in vec3 TexCoords;

uniform samplerCube skybox; // wyh

void main()
{    
    FragColor = texture(skybox, TexCoords); // wyh
}