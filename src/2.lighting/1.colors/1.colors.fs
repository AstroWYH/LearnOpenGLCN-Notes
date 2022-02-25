#version 330 core
out vec4 FragColor;
  
uniform vec3 objectColor;
uniform vec3 lightColor;

void main()
{
    FragColor = vec4(lightColor * objectColor, 1.0);
    // wyh 光源颜色 物体颜色 但是光源白立方体不是已经在1.light_cube.fs里写为1?
}
// wyh 物体的颜色