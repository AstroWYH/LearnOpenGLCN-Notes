#version 330 core
out vec4 FragColor;

in vec3 Normal;  
in vec3 FragPos;  
  
uniform vec3 lightPos; 
uniform vec3 viewPos; // wyh 比2.1新增观测坐标, 即摄像机坐标
uniform vec3 lightColor;
uniform vec3 objectColor;

void main()
{
    // ambient
    float ambientStrength = 0.1;
    vec3 ambient = ambientStrength * lightColor;
  	
    // diffuse 
    vec3 norm = normalize(Normal);
    vec3 lightDir = normalize(lightPos - FragPos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = diff * lightColor;
    
    // specular // wyh 比2.1增加高光项
    float specularStrength = 0.5;
    vec3 viewDir = normalize(viewPos - FragPos); // wyh 观测方向向量
    vec3 reflectDir = reflect(-lightDir, norm); // wyh 镜面反射光线, -是要求
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32); // wyh 32是余弦强度绘制的p
    vec3 specular = specularStrength * spec * lightColor; // wyh 高光项的总包含
    // wyh 高光强度 镜面反射和观测方向角度 光源本身强度
        
    vec3 result = (ambient + diffuse + specular) * objectColor;
    FragColor = vec4(result, 1.0);
}
// wyh 重点逐渐来到fs, cpp内2.2和2.1几乎无区别