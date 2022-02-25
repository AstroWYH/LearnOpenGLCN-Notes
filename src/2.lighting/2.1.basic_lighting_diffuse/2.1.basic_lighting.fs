#version 330 core
out vec4 FragColor;

in vec3 Normal; 
in vec3 FragPos; // wyh 接收vs来的out
  
uniform vec3 lightPos; // wyh 固定值, 难道是点光源?
uniform vec3 lightColor; // wyh why light's pos & color defined here?
uniform vec3 objectColor; // wyh 物体本来的颜色

void main()
{
    // ambient
    float ambientStrength = 0.1;
    vec3 ambient = ambientStrength * lightColor; // wyh 环境光没什么计算含量
  	
    // diffuse 
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(lightPos - FragPos); // wyh 这里的值都是可计算的
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = diff * lightColor; // wyh lightColor means I/r2? now r = 1
 
    vec3 result = (ambient + diffuse) * objectColor; // wyh objectColor means Ka or Kd?
    FragColor = vec4(result, 1.0); // wyh 最终给到片元着色器, 因为每个顶点一个法线, 所以逐顶点着色
} 