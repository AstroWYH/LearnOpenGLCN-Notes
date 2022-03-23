#version 330 core
layout (location = 0) in vec3 aPos;

uniform mat4 lightSpaceMatrix;
uniform mat4 model;

void main()
{
    gl_Position = lightSpaceMatrix * model * vec4(aPos, 1.0); // wyh 写深度值, 需要转到光源空间(以光源为摄像机的裁剪空间), 这样得到的深度值才是对光源来说的深度值
}
// wyh 毫无区别