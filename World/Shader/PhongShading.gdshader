shader_type spatial;
//render_mode depth_draw_opaque;

uniform bool enable = true;
uniform float roughness: hint_range(0.0, 1.0);
uniform float metallic: hint_range(0.0, 1.0);
uniform float specular: hint_range(0.0, 1.0) = 1.0;
uniform float diffuse_reflect = 1.0;
uniform float ambient_reflect = 0.05;
uniform float specular_reflect = 0.8;
uniform float specular_power: hint_range(1.0, 256.0, 1.0) = 10.0;

group_uniforms Color;
uniform bool use_texture = true;
uniform sampler2D color_texture: source_color;
uniform vec4 color: source_color;

varying vec3 normal;

void fragment() {
	if (use_texture) {
		ALBEDO = texture(color_texture, UV).xyz;
	} else {
		ALBEDO = color.rgb;
		ALPHA = color.a;
	}

	ROUGHNESS = roughness;
	METALLIC = metallic;
	SPECULAR = specular;
}

void vertex() {
	if (enable) {
		normal = normalize(MODELVIEW_NORMAL_MATRIX * NORMAL);
	}
}

void light() {
	if (enable) {
		// Ambient light
		vec3 ambient_light = LIGHT_COLOR / PI * ambient_reflect * ATTENUATION;

		// Diffuse light
		vec3 diffuse_light = max(dot(LIGHT, normal), 0.0) * diffuse_reflect *
					ATTENUATION * LIGHT_COLOR / PI;

		// Specular reflection
		vec3 reflection = (-LIGHT + 2.0 * dot(LIGHT, NORMAL) * NORMAL);
		vec3 specular_highlight = LIGHT_COLOR / PI * SPECULAR_AMOUNT *
					specular_reflect * pow(max(dot(reflection, VIEW), 0.0), specular_power) *
					ATTENUATION;

		DIFFUSE_LIGHT += ambient_light + diffuse_light + specular_highlight;
	}
}
