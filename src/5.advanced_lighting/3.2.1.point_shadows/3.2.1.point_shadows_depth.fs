#version 330 core
in vec4 FragPos;

uniform vec3 lightPos;
uniform float far_plane;

void main()
{
    float lightDistance = length(FragPos.xyz - lightPos);
    
    // map to [0;1] range by dividing by far_plane
    lightDistance = lightDistance / far_plane; // wyh 光照距离改成[0, 1]范围内
    
    // write this as modified depth
    gl_FragDepth = lightDistance; // wyh [0, 1]的光照距离用来当深度?这是个什么深度? 光源到物体表面像素的距离, 3.3是点光源在中心, 立方体深度贴图
}